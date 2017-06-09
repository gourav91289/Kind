using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OmniPot.Models.StateRegistrationViewModels;
using OmniPot.Data;
using OmniPot.Data.Models;
using Microsoft.EntityFrameworkCore;
using OmniPot.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using OmniPot.Data.Identity;
using System.Text;
using Microsoft.Extensions.Logging;

namespace OmniPot.Controllers
{
    [Produces("application/json")]
    [Route("api/StateRegistration")]
    [Authorize]
    public class StateRegistrationController : Controller
    {
        private readonly KindDbContext context;
        private readonly IPaymentService paymentService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;
        private readonly ILogger logger;
        //HACK: Hardcoded state for Rhode Island
        private readonly Guid RiId = Guid.Parse("8658118C-E757-44C3-80BE-E2B15F749F7C");

        public StateRegistrationController(KindDbContext context,
            IPaymentService paymentService,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            this.context = context;
            this.paymentService = paymentService;
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.signInManager = signInManager;
            logger = loggerFactory.CreateLogger<StateRegistrationController>();
        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await userManager.FindByIdAsync(userManager.GetUserId(HttpContext.User));
        }

        private Guid GetCurrentUserIdAsync()
        {
            return Guid.Parse(userManager.GetUserId(HttpContext.User));
        }

        [Route("AddBusiness")]
        [HttpPost]
        public async Task<IActionResult> AddBusiness([FromBody]AddBusinessViewModel model)
        {
            logger.LogDebug("Entering AddBussiness");
            if (!ModelState.IsValid)
                return BadRequest(new { Success = false, Message = ModelState });
            if (!IsValidZipCode(model.PostalCode))
                return BadRequest(new { Success = false, Message = "Invalid Zip Code." });
            if (!IsValidZipCode(model.GrowAddressPostalCode))
                return BadRequest(new { Success = false, Message = "Invalid Grow Address Zip Code." });

            var userId = GetCurrentUserIdAsync();

            var person = new Person
            {
                Address = new Address
                {
                    Addressee = model.BusinessName,
                    DeliveryLine1 = model.StreetAddressPrimary,
                    DeliveryLine2 = model.StreetAddressSecondary,
                    CityName = model.City,
                    PostalCode = model.PostalCode,
                    StateOrProvinceId = model.StateId,
                    State = TrackableEntityState.IsActive,
                    CreatedByUserId = userId
                },
                GrowAddress = new Address
                {
                    Addressee = string.Format("{0}, {1}", model.LastName, model.FirstName),
                    DeliveryLine1 = model.GrowAddressPrimary,
                    DeliveryLine2 = model.GrowAddressSecondary,
                    CityName = model.GrowAddressCity,
                    StateOrProvinceId = model.GrowAddressStateId,
                    PostalCode = model.GrowAddressPostalCode,
                    State = TrackableEntityState.IsActive,
                    CreatedByUserId = userId
                },
                IsGrowAddressCooperative = model.GrowAddressIsCooperative,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CompanyName = model.BusinessName,
                EmailAddress = model.EmailAddress,
                PhoneNumbers = new List<Phone> { new Phone { PhoneNumber = model.PhoneNumber, State = TrackableEntityState.IsActive, CreatedByUserId = userId } }
                ,
                Licenses = new List<License> { new License { LicenseNumber = model.LicenseNumber, State = TrackableEntityState.IsActive, ExpiryMonth = model.LicenseExpiryMonth, ExpiryYear = model.LicenseExpiryYear, CreatedByUserId = userId } },
                ApplicationStatus = await GetTemporyApplicationStatus(model.LicenseNumber, model.LicenseExpiryMonth, model.LicenseExpiryYear),
                State = TrackableEntityState.IsActive,
                HasDohDiscount = model.HasDohDiscount,
                PersonType = PersonType.Business,
                CreatedByUserId = userId
            };

            context.People.Add(person);

            await context.SaveChangesAsync();

            return Ok(person);

        }

        [Route("AddCaregiver")]
        [HttpPost]
        public async Task<IActionResult> AddCaregiver([FromBody]AddCaregiverViewModel model)
        {
            logger.LogDebug("Entering AddCargiver");
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid modelstate", model);
                return BadRequest(new { Success = false, Message = ModelState });
            }

            var patientLicense = await context.StateLicenses.Where(l => l.LicenseNumber == model.PatientLicenseNumber).FirstOrDefaultAsync();
            if (null == patientLicense)
            {
                logger.LogWarning("Invalid patient license", model);
                return BadRequest(new { Sucess = false, Message = "Invalid patient license." });
            }

            var userId = GetCurrentUserIdAsync();
            var tempApplicationStatus = ApplicationStatus.Pending;

            var stateLicense = await context.StateLicenses.Where(l => l.LicenseNumber == model.LicenseNumber).FirstOrDefaultAsync();
            if (null != stateLicense)
            {
                logger.LogInformation($"License found Number: {stateLicense.LicenseNumber} HasDiscount: {stateLicense.IsMedicaid}");
                model.HasDohDiscount = stateLicense.IsMedicaid;
                tempApplicationStatus = stateLicense.Expiry.Month == model.LicenseExpiryMonth && stateLicense.Expiry.Year == model.LicenseExpiryYear ? ApplicationStatus.Pending : ApplicationStatus.Pending;
            }

            var person = new Person
            {
                Address = new Address
                {
                    Addressee = string.Format("{0}, {1}", model.LastName, model.FirstName),
                    DeliveryLine1 = model.StreetAddressPrimary,
                    DeliveryLine2 = model.StreetAddressSecondary,
                    CityName = model.City,
                    StateOrProvinceId = model.StateId,
                    PostalCode = model.PostalCode,
                    State = TrackableEntityState.IsActive,
                    CreatedByUserId = userId
                },
                GrowAddress = new Address
                {
                    Addressee = string.Format("{0}, {1}", model.LastName, model.FirstName),
                    DeliveryLine1 = model.GrowAddressPrimary,
                    DeliveryLine2 = model.GrowAddressSecondary,
                    CityName = model.GrowAddressCity,
                    StateOrProvinceId = model.GrowAddressStateId,
                    PostalCode = model.GrowAddressPostalCode,
                    State = TrackableEntityState.IsActive,
                    CreatedByUserId = userId
                },
                IsGrowAddressCooperative = model.GrowAddressIsCooperative,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumbers = new List<Phone>
                {
                    new Phone { PhoneNumber = model.PhoneNumber, State = TrackableEntityState.IsActive, CreatedByUserId = userId }
                },
                ApplicationStatus = tempApplicationStatus,
                State = TrackableEntityState.IsActive,
                HasDohDiscount = model.HasDohDiscount,
                IsAlsoPatient = model.IsAlsoPatient,
                PersonType = PersonType.Caretaker,
                EmailAddress = model.EmailAddress,
                Licenses = new List<License> { new License { LicenseNumber = model.LicenseNumber, ExpiryMonth = model.LicenseExpiryMonth, ExpiryYear = model.LicenseExpiryYear, State = TrackableEntityState.IsActive, CreatedByUserId = userId } },
                PatientLicense = new License { LicenseNumber = model.PatientLicenseNumber, ExpiryMonth = model.PatientLicenseExpiryMonth, ExpiryYear = model.PatientLicenseExpiryYear, State = TrackableEntityState.IsActive, CreatedByUserId = userId },
                CreatedByUserId = userId
            };

            context.People.Add(person);

            await context.SaveChangesAsync();

            logger.LogInformation("Added person", person);

            return Ok(person);
        }

        [Route("AddIndividual")]
        [HttpPost]
        public async Task<IActionResult> AddIndividual([FromBody]AddIndividualViewModel model)
        {
            logger.LogDebug("Entering AddIndividual");

            if (!ModelState.IsValid)
            {
                logger.LogWarning("ModelState is invalid", model);
                return BadRequest(new { Success = false, Message = ModelState });
            }
            //if (!IsValidZipCode(model.PostalCode))
            //    return BadRequest(new { Success = false, Message = "Invalid Zip Code." });
            //if (!IsValidZipCode(model.GrowAddressPostalCode))
            //    return BadRequest(new { Success = false, Message = "Invalid Grow Address Zip Code." });

            var userId = GetCurrentUserIdAsync();
            var tempApplicationStatus = ApplicationStatus.Pending;

            var stateLicense = await context.StateLicenses.Where(l => l.LicenseNumber == model.LicenseNumber).FirstOrDefaultAsync();
            if (null != stateLicense)
            {
                logger.LogInformation($"Found License Number: {stateLicense.LicenseNumber} HasDohDiscount: {stateLicense.IsMedicaid}");
                model.HasDohDiscount = stateLicense.IsMedicaid;
                tempApplicationStatus = stateLicense.Expiry.Month == model.LicenseExpiryMonth && stateLicense.Expiry.Year == model.LicenseExpiryYear ? ApplicationStatus.Pending : ApplicationStatus.Pending;
                //_logger.LogInformation(5, "License Number  " + model.LicenseNumber + " eligible for reduced fee.");
            }

            var person = new Person
            {
                Address = new Address
                {
                    Addressee = string.Format("{0}, {1}", model.LastName, model.FirstName),
                    DeliveryLine1 = model.StreetAddressPrimary,
                    DeliveryLine2 = model.StreetAddressSecondary,
                    CityName = model.City,
                    StateOrProvinceId = model.StateId,
                    PostalCode = model.PostalCode,
                    State = TrackableEntityState.IsActive,
                    CreatedByUserId = userId
                },
                GrowAddress = new Address
                {
                    Addressee = string.Format("{0}, {1}", model.LastName, model.FirstName),
                    DeliveryLine1 = model.GrowAddressPrimary,
                    DeliveryLine2 = model.GrowAddressSecondary,
                    CityName = model.GrowAddressCity,
                    StateOrProvinceId = model.GrowAddressStateId,
                    PostalCode = model.GrowAddressPostalCode,
                    State = TrackableEntityState.IsActive,
                    CreatedByUserId = userId
                },
                IsGrowAddressCooperative = model.GrowAddressIsCooperative,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                PhoneNumbers = new List<Phone>
                {
                    new Phone { PhoneNumber = model.PhoneNumber, State = TrackableEntityState.IsActive, CreatedByUserId = userId }
                },
                Licenses = new List<License> { new License { LicenseNumber = model.LicenseNumber, State = TrackableEntityState.IsActive, ExpiryMonth = model.LicenseExpiryMonth, ExpiryYear = model.LicenseExpiryYear, CreatedByUserId = userId } },
                //TODO: The application status will need to be set depending on whether we have a valid license or not. 
                ApplicationStatus = tempApplicationStatus,
                State = TrackableEntityState.IsActive,
                HasDohDiscount = model.HasDohDiscount,
                PersonType = PersonType.Individual,
                CreatedByUserId = userId
            };
            context.People.Add(person);

            await context.SaveChangesAsync();

            logger.LogInformation("Added Person", person);

            return Ok(person);
        }

        private async Task<ApplicationStatus> GetTemporyApplicationStatus(string licenseNumber, int expiryMonth, int expiryYear)
        {
            logger.LogDebug("Entering GetTemporaryApplicationStatus");
            var license = await context.StateLicenses.Where(l => l.LicenseNumber == licenseNumber).FirstOrDefaultAsync();
            if (null != license && license.Expiry.Month == expiryMonth && license.Expiry.Year == expiryYear)
            {
                logger.LogInformation("Application Status Approved.");
                return ApplicationStatus.Approved;
            }
            logger.LogInformation("Application Status Pending.");
            return ApplicationStatus.Pending;
        }

        private bool IsValidZipCode(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            return (input.Length != 5); // || input.Substring(0, 3) == "028" || input.Substring(0, 3) == "029");
        }

        [Route("GetAccount")]
        [HttpPost]
        public async Task<IActionResult> GetAccount([FromBody]GetAccountViewModel model)
        {
            logger.LogDebug("Entering GetAccount");
            var account = await context.People.Include(p => p.Address).Where(p => p.State == TrackableEntityState.IsActive && p.PersonId == model.PersonId).AsNoTracking().FirstOrDefaultAsync();

            if (null == account)
            {
                logger.LogInformation("Account not found.", model);
                return NotFound();
            }
            logger.LogInformation("Found account:", model);
            return Ok(account);
        }

        [Route("ValidateLicense")]
        [HttpPost]
        public async Task<IActionResult> ValidateLicense([FromBody]ValidateLicenseViewModel model)
        {
            logger.LogDebug("Entering ValidateLicence");
            if (!ModelState.IsValid)
            {
                logger.LogWarning("ModelState is invalid", model);
                return BadRequest(new { Success = false, Message = ModelState });
            }

            if (!string.IsNullOrEmpty(model.CaretakerLicense))
            {
                logger.LogDebug("Checking caretaker license");
                var license = await context.StateLicenses.Where(l => l.LicenseNumber == model.LicenseNumber && l.CaretakerLicenseNumber == model.CaretakerLicense).FirstOrDefaultAsync();
                if (null != license)
                {
                    if (model.CaretakerLicense.ToUpper() != license.CaretakerLicenseNumber.ToUpper())
                    {
                        logger.LogWarning("Caretaker license doesn't match patient", model);
                        return BadRequest(new { Success = false, Message = "Invalid caretaker license" });
                    }
                    if (model.CaretakerLicenseExpiryMonth != license.CaretakerExpiry.Month || model.CaretakerLicenseExpiryYear != license.CaretakerExpiry.Year)
                    {
                        logger.LogWarning("Caretaker license has invalid expiry", model);
                        return BadRequest(new { Success = false, Message = "Invalid caretaker license" });
                    }
                    if (license.CaretakerExpiry < DateTime.Now)
                    {
                        logger.LogWarning("Caretaker license has expired", model);
                        return BadRequest(new { Success = false, Message = "Caretaker license has expired." });
                    }
                    return Ok(new { Success = true, LicenseNumber = model.LicenseNumber });
                } 
            }
            else
            {
                var license = await context.StateLicenses.Where(l => l.LicenseNumber == model.LicenseNumber).FirstOrDefaultAsync();
                if (null != license)
                {
                    if (license.Expiry < DateTime.Now)
                    {
                        logger.LogWarning("This license is expired.", model);
                        return BadRequest(new { Success = false, Message = "The requested license is expired." });
                    }

                    if (license.Expiry.Month != model.LicenseExpiryMonth || license.Expiry.Year != model.LicenseExpiryYear)
                    {
                        logger.LogWarning("This license failed to validate", model);
                        return BadRequest(new { Success = false, Message = "License failed to validate." });
                    }
                    return Ok(new { Success = true, LicenseNumber = model.LicenseNumber });
                }
            }

            logger.LogWarning("License number not found.", model);
            return NotFound(model.LicenseNumber);
        }

        [Route("AddTags")]
        [HttpPost]
        public async Task<IActionResult> AddTags([FromBody]AddTagsViewModel model)
        {
            logger.LogDebug("Entering AddTags");
            if (!ModelState.IsValid)
            {
                logger.LogWarning("ModelState is invalid.", model);
                return BadRequest(new { Success = false, Message = ModelState });
            }

            var newOrderId = Guid.NewGuid();
            var order = await context.PlantTagOrders.Include(t => t.OrderItems).Where(t => t.State == TrackableEntityState.IsActive && t.PersonId == model.PersonId).DefaultIfEmpty(new PlantTagOrder
            {
                PersonId = model.PersonId,
                OrderDateUtc = DateTime.UtcNow,
                TagQuantity = model.Quantity,
                State = TrackableEntityState.IsActive,
                PlantTagOrderId = newOrderId
            }).FirstOrDefaultAsync();

            logger.LogInformation("Found an order.", order);
            //TODO: any logic that checks that this person/business can add tags. 

            var person = await context.People.Where(p => p.State == TrackableEntityState.IsActive && p.PersonId == model.PersonId).FirstOrDefaultAsync();
            if (null == person)
            {
                logger.LogWarning("Could not find person.", model);
                return BadRequest(new { Sucess = false, Message = "Person not valid." });
            }
            else
            {
                person.ApplicationStatus = ApplicationStatus.Pending;
                logger.LogInformation($"Setting application status {person.ApplicationStatus}:", person);
            }

            if (person.PersonType != PersonType.Business && model.Quantity > 12)
            {
                logger.LogWarning("Customer specified quantity over 12");
                return BadRequest(new { Sucess = false, Message = "Invalid quantity specified" });
            }

            var currentTagCount = await context.PlantTagOrders.Where(p => p.PersonId == model.PersonId && p.State == TrackableEntityState.IsActive).Include(o => o.OrderItems).SelectMany(o => o.OrderItems).SumAsync(o => o.Quantity);
            if (person.IsGrowAddressCooperative && currentTagCount + model.Quantity > 48)
            {
                logger.LogWarning("Grow quanity exceeds that of a co-op");
                return BadRequest(new { Success = false, Message = "Quantity exceeds allowed total (48)" });
            }
            if (person.IsAlsoPatient && currentTagCount + model.Quantity > 24)
            {
                logger.LogWarning("Grow quantity exceeds that of a caretaker");
                return BadRequest(new { Success = false, Message = "Quantity exceeds allowed total (24)" });
            }
            if (currentTagCount + model.Quantity > 12)
            {
                logger.LogWarning("Grow quantity exceeds that of a patient");
                return BadRequest(new { Success = false, Message = "Quantity exceeds allowed total (12)" });
            }

#if DEBUG
            decimal tagPrice = 73.82m;
#else
            decimal tagPrice = person.HasDohDiscount ? 0m : 25m;
#endif
            //If caregiver, check the patient license for 
            //if (person.PersonType == PersonType.Caretaker)
            //{
            var patientLicense = await context.StateLicenses.Where(l => l.LicenseNumber == model.LicenseNumber).FirstOrDefaultAsync();
            if (null != patientLicense && patientLicense.IsMedicaid)
                tagPrice = 0m;
            //}

            string tagSku = "000100";

            if (person.PersonType == PersonType.Business)
            {
                tagPrice = model.TagType == TagType.Plant ? .37m : .22m;
                tagSku = model.TagType == TagType.Plant ? "000200" : "000300";
            }

            var orderItem = new PlantTagOrderItem { PlantTagOrderItemId = Guid.NewGuid(), PlantTagOrderId = order.PlantTagOrderId, State = TrackableEntityState.IsActive, TagType = model.TagType, Quantity = model.Quantity, Price = tagPrice, TotalAmount = model.Quantity * tagPrice, AddressId = model.AddressId, Sku = tagSku };
            //if (order.HandlingFee != 2.5m) order.HandlingFee = 2.5m; 

            order.OrderItems.Add(orderItem);
            //#if DEBUG
            //            order.Amount = 73.82m;
            //#else
            order.Amount += orderItem.TotalAmount;
            //#endif
            if (order.PlantTagOrderId == newOrderId)
                context.PlantTagOrders.Add(order);

            logger.LogInformation("The order I've created is:", order);

            await context.SaveChangesAsync();

            return Ok(orderItem);
        }

        [Route("RemoveOrderItem")]
        [HttpPost]
        public async Task<IActionResult> RemoveOrderItem([FromBody]RemoveOrderItemViewModel model)
        {
            logger.LogDebug("Entering RemoveOrderItem");
            if (!ModelState.IsValid)
            {
                logger.LogWarning("ModelState is invalid", model);
                return BadRequest(new { Success = false, Message = ModelState });
            }

            var orderItem = await context.PlantTagOrderItems.Where(p => p.PlantTagOrderId == model.OrderId && p.PlantTagOrderItemId == model.OrderItemId).FirstOrDefaultAsync();
            if (null != orderItem)
            {
                orderItem.State = TrackableEntityState.IsDeleted;
                context.Remove(orderItem);
                await context.SaveChangesAsync();
                logger.LogInformation("OrderItem removed", model);
                return Ok(orderItem);
            }
            logger.LogWarning("OrderItem not found", model);
            return NotFound(model);
        }

        [Route("GetCurrentOrder")]
        [HttpPost]
        public async Task<IActionResult> GetCurrentOrder([FromBody]GetCurrentOrderViewModel model)
        {
            logger.LogDebug("Entering GetCurrentOrder");
            if (!ModelState.IsValid)
            {
                logger.LogWarning("ModelState is invalid", model);
                return BadRequest(new { Success = false, Message = ModelState });
            }

            var order = await context.PlantTagOrders.Include(t => t.OrderItems).Where(t => t.State == TrackableEntityState.IsActive && t.PersonId == model.PersonId).DefaultIfEmpty(new PlantTagOrder
            {
                PersonId = model.PersonId,
                OrderDateUtc = DateTime.UtcNow,
                State = TrackableEntityState.IsActive
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();

            logger.LogInformation("Found Order:", order);

            return Ok(order);
        }

        [HttpPost("GetOrderSummary")]
        public async Task<IActionResult> GetOrderSummary([FromBody]GetOrderSummaryViewModel model)
        {
            logger.LogDebug("Entering GetOrderSummary");

            if (!ModelState.IsValid)
            {
                logger.LogWarning("ModelState is invalid", model);
                return BadRequest(new { Success = false, Message = ModelState });
            }

            var order = await context.PlantTagOrders.Include(o => o.OrderItems).Where(o => o.State == TrackableEntityState.IsActive
                && o.PlantTagOrderId == model.OrderId && o.PersonId == model.PersonId)
                .Select(o => new OrderSummaryViewModel
                {
                    PersonId = o.PersonId,
                    OrderId = o.PlantTagOrderId,
                    OrderDateUtc = o.OrderDateUtc,
                    ConfirmationNumber = o.PlantTagOrderId.ToString().Substring(0, 8),
                    OrderItems = o.OrderItems.Select(oi => new OrderItemViewModel
                    {
                        OrderItemId = oi.PlantTagOrderItemId,
                        Price = oi.Price,
                        TagCount = oi.Quantity,
                        TagType = oi.TagType.ToString(),
                        LineTotal = oi.TotalAmount
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (null != order)
            {
                logger.LogInformation("I found an order to return:", order);
                return Ok(order);
            }

            logger.LogWarning("Sorry, couldn't find that order:", order);
            return BadRequest(model);
        }


        [HttpPost("GetSingleOrder")]
        public async Task<IActionResult> GetSingleOrder([FromBody]GetPendingOrdersViewModel model)
        {
            logger.LogDebug("Entering GetSingleOrder");
            if (!ModelState.IsValid)
            {
                logger.LogWarning("ModelState is invalid", model);
                return BadRequest(new { Success = false, Message = ModelState });
            }

            var pendingOrders = await (from p in context.People
                                       where p.ApplicationStatus == ApplicationStatus.Pending && p.CreatedByUserId == model.UserId
                                       join op in context.PlantTagOrders on p.PersonId equals op.PersonId
                                       join ot in context.PlantTagOrderItems on op.PlantTagOrderId equals ot.PlantTagOrderId
                                       select new
                                       {
                                           PersonID = p.PersonId,
                                           OrderID = op.PlantTagOrderId.ToString().Substring(0, 8),
                                           OrderDate = op.OrderDateUtc.ToLocalTime().ToString("MM/dd/yyyy hh:mm tt"),
                                           FirstName = p.FirstName,
                                           LastName = p.LastName,
                                           //Address = p.Address,
                                           //GrowAddress = p.GrowAddress,
                                           ExpiryUtc = DateTime.UtcNow.ToLocalTime().ToString("MM/dd/yyyy"),
                                           LicenseNumber = p.Licenses.FirstOrDefault().LicenseNumber,
                                           Quantity = op.TagQuantity,
                                           TotalAmount = ot.TotalAmount,
                                           PersonType = (Convert.ToInt32(p.PersonType) != 1) ? "Patient" : "Caregiver"
                                       })
                          .OrderByDescending(o => o.OrderDate)
                          .FirstOrDefaultAsync();

            if (null != pendingOrders)
            {
                logger.LogInformation("Found pending orders:", pendingOrders);
                return Ok(pendingOrders);
            }

            logger.LogWarning("No pending orders found: ", model);
            return BadRequest("No Pending Orders Found.");
        }

        [HttpPost("GetPendingOrders")]
        public async Task<IActionResult> GetPendingSummary([FromBody]GetPendingOrdersViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Success = false, Message = ModelState });
            var pendingOrders = await (from p in context.People
                                       where p.ApplicationStatus != 0 && p.CreatedByUserId == model.UserId
                                       join op in context.PlantTagOrders on p.PersonId equals op.PersonId
                                       join ot in context.PlantTagOrderItems on op.PlantTagOrderId equals ot.PlantTagOrderId
                                       select new
                                       {
                                           PersonID = p.PersonId,
                                           ConfimationNumber = op.PlantTagOrderId.ToString().Substring(0, 8),
                                           OrderID = op.PlantTagOrderId,
                                           OrderItemID = ot.PlantTagOrderItemId,
                                           OrderDate = op.OrderDateUtc.ToLocalTime().ToString("MM/dd/yyyy hh:mm tt"),
                                           FirstName = p.FirstName,
                                           LastName = p.LastName,
                                           ExpiryUtc = DateTime.UtcNow.ToLocalTime().ToString("MM/dd/yyyy"),
                                           LicenseNumber = p.Licenses.FirstOrDefault().LicenseNumber,
                                           Quantity = ot.Quantity,
                                           TotalAmount = ot.TotalAmount,
                                           PersonType = (Convert.ToInt32(p.PersonType) == 1) ? "Patient" : "Caregiver",
                                           OrderStatus = ot.State,
                                           AddressID = ot.AddressId,
                                           ApplicationStatus = (Convert.ToInt32(p.ApplicationStatus) == 4) || (Convert.ToInt32(p.ApplicationStatus) == 8) ? "<td> <a id='btnEdit' data-target='#modal-dialog-approve' data-toggle='modal' class='btn btn-success btn-xs'><i ng-if='ApplicationStatus == 8' class='fa fa-edit fa-fw'></i> Modify</a></td>" : "<b> Order Processing</b>"
                                       })
                          .OrderByDescending(o => o.OrderDate)

                          .ToListAsync();

            if (null != pendingOrders)
                return Ok(pendingOrders);

            return BadRequest("No Pending Orders Found.");
        }

        [Route("ModifyOrder")]
        [HttpPost]
        public async Task<IActionResult> ModifyOrder([FromBody]ModifyOrderViewModel model)
        {
            try
            {
                var orderItem = await context.PlantTagOrderItems.Where(t => t.State == TrackableEntityState.IsActive && t.PlantTagOrderId == model.OrderId && t.PlantTagOrderItemId == model.OrderItemId)
                    .AsNoTracking()
               .FirstOrDefaultAsync();

                if (null != orderItem)
                {
                    orderItem.Quantity = model.Quantity;
                    orderItem.TotalAmount = model.TotalAmount;

                    context.Entry(orderItem).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return Ok(orderItem);
                }

                return NotFound(model);

            }
            catch (Exception ex)
            {
                logger.LogCritical("There was an exception modifying the order:", ex);
                return BadRequest(new { Success = false, Message = "Exception while preparing payment" });
            }
        }

        [Route("Checkout")]
        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody]CheckoutViewModel model)
        {
            try
            {
                //Build a prepare payment object from the current order.
                var person = await context.People.Include(p => p.PhoneNumbers).Include(p => p.Address).ThenInclude(p => p.StateOrProvince).Where(p => p.State == TrackableEntityState.IsActive && p.PersonId == model.PersonId).FirstOrDefaultAsync();
                if (null == person)
                    return BadRequest(new { Success = false, Message = ModelState });

                person.ApplicationStatus = ApplicationStatus.AwaitingPayment;
                await context.SaveChangesAsync();

                var order = await context.PlantTagOrders.Include(t => t.OrderItems).Where(t => t.State == TrackableEntityState.IsActive && t.PersonId == model.PersonId)
                    .AsNoTracking()
               .FirstOrDefaultAsync();

                if (null == order)
                    return BadRequest(new { Success = false, Message = "Order is not available for payment." });

#if DEBUG
                var urlHost = "https://staging.rimmptags.com";
#elif RELEASE
                var urlHost = "https://www.rimmptags.com";
#else
                var urlHost = "http://localhost:2512";
#endif


                var request = new PreparePaymentRequest
                {
                    Address1 = person.Address.DeliveryLine1,
                    Address2 = person.Address.DeliveryLine2,
                    City = person.Address.CityName,
                    State = person.Address.StateOrProvince.Abbreviation,
                    Zip = person.Address.PostalCode,
                    CompanyName = person.CompanyName,
                    Country = "US",
                    CustomerId = person.PersonId.ToString(),
                    Email = person.EmailAddress,
                    Name = string.Format("{0} {1}", person.FirstName, person.LastName),
                    Phone = person.PhoneNumbers.FirstOrDefault().PhoneNumber,
                    PlantTagOrderId = order.PlantTagOrderId,
                    //TODO: These need to be changed to the actual urls. These should just redirect to the home page of the app. 
                    //      there needs to be another method that calls a complete order method in the api that changes the 
                    //      order status appropriately (to paid)
                    HrefSuccess = urlHost + $"/#/app/layout/confirmation",
                    HrefCancel = urlHost + $"/#/app/layout/cancelPayment",
                    HrefDuplicate = urlHost + $"/#/app/layout/duplicateOrder",
                    HrefFailure = urlHost + $"/#/app/layout/paymentFailure",


                };

                foreach (var item in order.OrderItems)
                {
                    if (item.State == TrackableEntityState.IsActive)
                        request.OrderItems.Add(new PreparePaymentRequestItem { Quantity = item.Quantity, UnitPrice = item.Price, Sku = item.Sku });
                }

                // Call the payment service to prepare the payment
                var response = paymentService.PreparePayment(request);
                // Return the url that the client will redirect to. We can't redirect here as it's an api call. 
                if (response.ErrorCode == 139)
                {
                    return Ok(response);
                }

                //person.ApplicationStatus = ApplicationStatus.AwaitingPickup;



                return Ok(response);

            }
            catch (Exception ex)
            {
                logger.LogCritical("There was an exception preparing payment:", ex);
                return BadRequest(new { Success = false, Message = "Exception while preparing payment" });
            }
        }

        [HttpPost("CompleteOrder")]
        public async Task<IActionResult> CompleteOrder([FromBody]CheckoutViewModel model)
        {
            var person = await context.People.Include(p => p.PhoneNumbers).Include(p => p.Address).ThenInclude(p => p.StateOrProvince).Where(p => p.State == TrackableEntityState.IsActive && p.PersonId == model.PersonId).FirstOrDefaultAsync();
            if (null == person)
                return BadRequest(new { Success = false, Message = ModelState });

            person.ApplicationStatus = ApplicationStatus.AwaitingShipment;
            await context.SaveChangesAsync();

            var order = await context.PlantTagOrders.Include(t => t.OrderItems).Where(t => t.State == TrackableEntityState.IsActive && t.PersonId == model.PersonId)
                   .AsNoTracking()
              .FirstOrDefaultAsync();

            if (null == order)
                return BadRequest("Order is not available for payment.");

            var mailBuilder = new StringBuilder($@"{person.FirstName} {person.LastName},
      Thank you for your order of medical marijuana plant tag sets on {DateTime.Now.ToString("MM/dd/yyyy")} from RI Department of Business Regulations. Your confirmation number is {order.PlantTagOrderId.ToString().Substring(0, 8)} Your order is being processed.  Print this confirmation page and confirmation number for your records. Please follow the link below to add additional tag sets up to your regulatory limit, or to review your order. You will be contacted when your tags are available for pickup.

https://www.rimmptags.com/#/login");

            await emailSender.SendEmailAsync(person.EmailAddress, "Tag order payment successful", mailBuilder.ToString());

            return Ok();
        }

        [HttpPost("CompletePayment")]
        public async Task<IActionResult> CompletePayment([FromBody]CompletePaymentViewModel model)
        {
            var order = await context.PlantTagOrders.Where(o => o.State == TrackableEntityState.IsActive && o.PlantTagOrderId == model.OrderId).FirstOrDefaultAsync();
            if (null == order)
                return BadRequest("Order not found");

            order.Token = model.PaymentToken;

            var person = await context.People.Where(p => p.State == TrackableEntityState.IsActive && p.PersonId == order.PersonId).FirstOrDefaultAsync();

            if (model.IsSuccessful)
                person.ApplicationStatus = ApplicationStatus.AwaitingShipment;
            //just to be sure
            else
                person.ApplicationStatus = ApplicationStatus.AwaitingShipment;

            await context.SaveChangesAsync();

            var mailBuilder = new StringBuilder($@"{person.FirstName} {person.LastName},
      Thank you for your order of medical marijuana plant tag sets on {DateTime.Now.ToString("MM/dd/yyyy")} from RI Department of Business Regulations. Your confirmation number is {order.PlantTagOrderId.ToString().Substring(0, 8)} Your order is being processed.  Print this confirmation page and confirmation number for your records. Please follow the link below to add additional tag sets up to your regulatory limit, or to review your order. You will be contacted when your tags are available for pickup.

https://www.rimmptags.com/#/login");

            //await emailSender.SendEmailAsync(person.EmailAddress, "Tag order payment successful", mailBuilder.ToString());

            return Ok();
        }

        [HttpPost("TempCheckout")]
        public async Task<IActionResult> TempCheckout([FromBody]CheckoutViewModel model)
        {
            //Build a prepare payment object from the current order.
            var person = await context.People.Include(p => p.Address).Where(p => p.State == TrackableEntityState.IsActive && p.PersonId == model.PersonId).AsNoTracking().FirstOrDefaultAsync();
            if (null == person)
                return BadRequest("Person is invalid.");

            person.ApplicationStatus = person.ApplicationStatus |= ApplicationStatus.AwaitingPayment;

            await context.SaveChangesAsync();

            var order = await context.PlantTagOrders.Include(o => o.OrderItems).Where(o => o.State == TrackableEntityState.IsActive && o.PlantTagOrderId == model.OrderId && o.PersonId == model.PersonId)
              .Select(o => new OrderSummaryViewModel
              {
                  PersonId = o.PersonId,
                  OrderId = o.PlantTagOrderId,
                  OrderDateUtc = o.OrderDateUtc,
                  ConfirmationNumber = o.PlantTagOrderId.ToString().Substring(0, 8),
                  OrderItems = o.OrderItems.Select(oi => new OrderItemViewModel
                  {
                      OrderItemId = oi.PlantTagOrderItemId,
                      Price = oi.Price,
                      TagCount = oi.Quantity,
                      TagType = oi.TagType.ToString(),
                      LineTotal = oi.TotalAmount
                  }).ToList()
              }).FirstOrDefaultAsync();


            if (null == order)
                return BadRequest("Order is not available for payment.");

            var mailBuilder = new StringBuilder(@"Thank You. Your order has been received, and is currently processing. You will be contacted by email to process payment once your order is approved. Please be sure to have your order information available to process this payment.

");
            mailBuilder.AppendLine("Order Summary");
            mailBuilder.AppendLine($"Order Confirmation Number: {order.ConfirmationNumber}\n");
            mailBuilder.AppendLine("Tag Type\t\tQuantity\t\tPrice");
            foreach (var item in order.OrderItems)
            {
                mailBuilder.AppendLine($"{item.TagType}\t\t{item.TagCount}\t\t{item.LineTotal} *");
            }
            mailBuilder.AppendLine("\n\n* Please note that the follow up contact for payment processing will account for any applicable fee reductions, and the total listed here will be adjusted as needed prior to that contact. No patient who qualifies for reduced fees will be charged for tags reserved here.\n");

            await emailSender.SendEmailAsync(person.EmailAddress, "Order Confirmation for the RI MM Plant Tag System ", mailBuilder.ToString());
            //TODO: Fix this such that we log it, don't currently have a logger
            //.ContinueWith(t => Console.WriteLine(t.Exception), TaskContinuationOptions.OnlyOnFaulted);

            //Removing this because the client should call it.
            //await signInManager.SignOutAsync();

            return Ok(new { Success = true, Message = "Message Confirmation Sent." });
        }


        [Route("ProvisionTags")]
        [HttpPost]
        //TODO: This will actually create a PlantTagOrder record prior to sending for payment. 
        [Authorize(Roles = "SuperAdmin,StateProcessor")]
        public async Task<IActionResult> ProvisionTags([FromBody]ProvisionTagsViewModel model)
        {
            throw new NotImplementedException("Tag provisioning not available in phase one.");
            if (!ModelState.IsValid)
                return BadRequest(new { Success = false, Message = ModelState });
            if (!context.AvailablePlantTags.Any())
                return StatusCode(500, "No tags available to provision.");
            var tags = context.AvailablePlantTags.Take(model.Quantity);
            foreach (var tag in tags)
            {
                tag.AddressId = model.AddressId;
                tag.IssuedUtc = DateTime.UtcNow;
                //TODO: Need expiry rules; 
                tag.ExpiryUtc = DateTime.UtcNow.AddYears(1);
            }

            var person = await context.People.Where(p => p.PersonId == model.PersonId && p.State == TrackableEntityState.IsActive).SingleAsync();
            //person.PlantTags.AddRange(tags);

            await context.SaveChangesAsync();
            return Ok(tags);
            //TODO: Create the data model for Person 1..* PlantTag
            //TODO: Determine whether we are pulling from a queue of available tags or creating/assigning
        }

        [Route("GetApprovalList")]
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,StateProcessor")]
        public async Task<IActionResult> GetApprovalList()
        {

            var people = await (from p in context.People
                                join op in context.PlantTagOrders on p.PersonId equals op.PersonId
                                join ot in context.PlantTagOrderItems on op.PlantTagOrderId equals ot.PlantTagOrderId
                                select new
                                {
                                    PersonID = p.PersonId,
                                    OrderID = op.PlantTagOrderId,
                                    OrderDate = op.OrderDateUtc.ToString("MM-dd-yyyy hh:mm tt"),
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    ExpiryUtc = DateTime.UtcNow.ToString("MM-dd-yyyy"),
                                    LicenseNumber = p.Licenses.FirstOrDefault().LicenseNumber,
                                    Status = p.ApplicationStatus,
                                    Quantity = op.TagQuantity,
                                    TotalAmount = ot.TotalAmount,
                                    CreatedAt = ot.CreatedUtc
                                }).Include(p => p.Status == ApplicationStatus.Pending)
                          .OrderByDescending(o => o.OrderDate)
                          .ToListAsync();

            return Ok(people);
        }



        [Route("GetCompleteList")]
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,StateProcessor, KindProcessor")]
        public async Task<IActionResult> GetCompleteList()
        {
            var people = await (from p in context.People
                                where p.ApplicationStatus == ApplicationStatus.Complete
                                join op in context.PlantTagOrders on p.PersonId equals op.PersonId
                                join ot in context.PlantTagOrderItems on op.PlantTagOrderId equals ot.PlantTagOrderId
                                join a in context.Addresses on p.AddressId equals a.AddressId
                                select new
                                {
                                    PersonID = p.PersonId,
                                    OrderID = op.PlantTagOrderId.ToString().Substring(0, 8).ToUpper(),
                                    OrderDate = op.OrderDateUtc.ToLocalTime().ToString("MM/dd/yyyy hh:mm tt"),
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    ExpiryUtc = DateTime.UtcNow.ToLocalTime().ToString("MM/dd/yyyy"),
                                    LicenseNumber = p.Licenses.FirstOrDefault().LicenseNumber,
                                    Quantity = ot.Quantity,
                                    TotalAmount = ot.TotalAmount,
                                    PersonType = (Convert.ToInt32(p.PersonType) != 1) ? "Patient" : "Caregiver",
                                    DeliveryLine1 = a.DeliveryLine1,
                                    City = a.CityName,
                                    PostalCode = a.PostalCode,
                                })
                          .OrderByDescending(o => o.OrderDate)
                          .ToListAsync();

            return Ok(people);
        }

        [Route("GetDenyList")]
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,StateProcessor,KindProcessor")]
        public async Task<IActionResult> GetDenyList()
        {
            var people = await (from p in context.People
                                where p.ApplicationStatus == ApplicationStatus.Denied
                                join op in context.PlantTagOrders on p.PersonId equals op.PersonId
                                join ot in context.PlantTagOrderItems on op.PlantTagOrderId equals ot.PlantTagOrderId
                                join a in context.Addresses on p.AddressId equals a.AddressId
                                select new
                                {
                                    PersonID = p.PersonId,
                                    OrderID = op.PlantTagOrderId.ToString().Substring(0, 8).ToUpper(),
                                    OrderDate = op.OrderDateUtc.ToLocalTime().ToString("MM/dd/yyyy hh:mm tt"),
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    ExpiryUtc = DateTime.UtcNow.ToLocalTime().ToString("MM/dd/yyyy"),
                                    LicenseNumber = p.Licenses.FirstOrDefault().LicenseNumber,
                                    Quantity = ot.Quantity,
                                    TotalAmount = ot.TotalAmount,
                                    PersonType = (Convert.ToInt32(p.PersonType) != 1) ? "Patient" : "Caregiver",
                                    DeliveryLine1 = a.DeliveryLine1,
                                    City = a.CityName,
                                    PostalCode = a.PostalCode,
                                })
                          .OrderByDescending(o => o.OrderDate)
                          .ToListAsync();

            return Ok(people);
        }


        [Route("GetPendingList")]
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,StateProcessor,KindProcessor")]
        public async Task<IActionResult> GetPendingList()
        {

            var people = await (from p in context.People
                                where p.ApplicationStatus == ApplicationStatus.Pending
                                join op in context.PlantTagOrders on p.PersonId equals op.PersonId
                                join ot in context.PlantTagOrderItems on op.PlantTagOrderId equals ot.PlantTagOrderId
                                join a in context.Addresses on p.AddressId equals a.AddressId
                                select new
                                {
                                    PersonID = p.PersonId,
                                    OrderID = op.PlantTagOrderId.ToString().Substring(0, 8).ToUpper(),
                                    OrderDate = op.OrderDateUtc.ToLocalTime().ToString("MM/dd/yyyy hh:mm tt"),
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    ExpiryUtc = DateTime.UtcNow.ToLocalTime().ToString("MM/dd/yyyy"),
                                    LicenseNumber = p.Licenses.FirstOrDefault().LicenseNumber,
                                    Quantity = ot.Quantity,
                                    TotalAmount = ot.TotalAmount,
                                    PersonType = (Convert.ToInt32(p.PersonType) != 1) ? "Caregiver" : "Patient",
                                    DeliveryLine1 = a.DeliveryLine1,
                                    City = a.CityName,
                                    PostalCode = a.PostalCode,
                                })
                          .OrderByDescending(o => o.OrderDate)
                          .ToListAsync();

            return Ok(people);
        }

        [Route("GetPaymentList")]
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,StateProcessor,KindProcessor")]
        public async Task<IActionResult> GetPaymentList()
        {

            var people = await (from p in context.People
                                where p.ApplicationStatus == ApplicationStatus.AwaitingPayment
                                join op in context.PlantTagOrders on p.PersonId equals op.PersonId
                                join ot in context.PlantTagOrderItems on op.PlantTagOrderId equals ot.PlantTagOrderId
                                join a in context.Addresses on p.AddressId equals a.AddressId
                                select new
                                {
                                    PersonID = p.PersonId,
                                    OrderID = op.PlantTagOrderId.ToString().Substring(0, 8).ToUpper(),
                                    OrderDate = op.OrderDateUtc.ToLocalTime().ToString("MM/dd/yyyy hh:mm tt"),
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    ExpiryUtc = DateTime.UtcNow.ToLocalTime().ToString("MM/dd/yyyy"),
                                    LicenseNumber = p.Licenses.FirstOrDefault().LicenseNumber,
                                    Quantity = ot.Quantity,
                                    TotalAmount = ot.TotalAmount,
                                    PersonType = (Convert.ToInt32(p.PersonType) != 1) ? "Caregiver" : "Patient",
                                    DeliveryLine1 = a.DeliveryLine1,
                                    City = a.CityName,
                                    PostalCode = a.PostalCode,
                                })
                          .OrderByDescending(o => o.OrderDate)
                          .ToListAsync();

            return Ok(people);
        }

        [Route("GetShippedList")]
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,StateProcessor,KindProcessor")]
        public async Task<IActionResult> GetShippedList()
        {
            var people = await (from p in context.People
                                where p.ApplicationStatus == ApplicationStatus.AwaitingShipment
                                join op in context.PlantTagOrders on p.PersonId equals op.PersonId
                                join ot in context.PlantTagOrderItems on op.PlantTagOrderId equals ot.PlantTagOrderId
                                join a in context.Addresses on p.AddressId equals a.AddressId
                                select new
                                {
                                    PersonID = p.PersonId,
                                    OrderID = op.PlantTagOrderId.ToString().Substring(0, 8).ToUpper(),
                                    OrderDate = op.OrderDateUtc.ToLocalTime().ToString("MM/dd/yyyy hh:mm tt"),
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    ExpiryUtc = DateTime.UtcNow.ToLocalTime().ToString("MM/dd/yyyy"),
                                    LicenseNumber = p.Licenses.FirstOrDefault().LicenseNumber,
                                    Quantity = ot.Quantity,
                                    TotalAmount = ot.TotalAmount,
                                    PersonType = (Convert.ToInt32(p.PersonType) != 1) ? "Caregiver" : "Patient",
                                    DeliveryLine1 = a.DeliveryLine1,
                                    City = a.CityName,
                                    PostalCode = a.PostalCode,
                                })
                          .OrderByDescending(o => o.OrderDate)
                          .ToListAsync();

            return Ok(people);
        }

        [Route("GetPickupList")]
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,StateProcessor,KindProcessor")]
        public async Task<IActionResult> GetPickupList()
        {
            var people = await (from p in context.People
                                where p.ApplicationStatus == ApplicationStatus.AwaitingPickup
                                join op in context.PlantTagOrders on p.PersonId equals op.PersonId
                                join ot in context.PlantTagOrderItems on op.PlantTagOrderId equals ot.PlantTagOrderId
                                join a in context.Addresses on p.AddressId equals a.AddressId
                                select new
                                {
                                    PersonID = p.PersonId,
                                    OrderID = op.PlantTagOrderId.ToString().Substring(0, 8).ToUpper(),
                                    OrderDate = op.OrderDateUtc.ToLocalTime().ToString("MM/dd/yyyy hh:mm tt"),
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    ExpiryUtc = DateTime.UtcNow.ToLocalTime().ToString("MM/dd/yyyy"),
                                    LicenseNumber = p.Licenses.FirstOrDefault().LicenseNumber,
                                    Quantity = ot.Quantity,
                                    TotalAmount = ot.TotalAmount,
                                    PersonType = (Convert.ToInt32(p.PersonType) != 1) ? "Caregiver" : "Patient",
                                    DeliveryLine1 = a.DeliveryLine1,
                                    City = a.CityName,
                                    PostalCode = a.PostalCode,
                                })
                          .OrderByDescending(o => o.OrderDate)
                          .ToListAsync();

            return Ok(people);
        }

        [Route("CheckOrder")]
        [HttpPost]
        public async Task<IActionResult> CheckOrder([FromBody]CheckOrderQtyViewModel model)
        {
            var list = await (from p in context.People
                              where p.ApplicationStatus == ApplicationStatus.Pending
                              join op in context.PlantTagOrders on p.PersonId equals op.PersonId
                              join ot in context.PlantTagOrderItems on op.PlantTagOrderId equals ot.PlantTagOrderId
                              select new
                              {
                                  PersonID = p.PersonId,
                                  OrderID = op.PlantTagOrderId,
                                  OrderDate = op.OrderDateUtc.ToLocalTime().ToString("MM/dd/yyyy hh:mm tt"),
                                  FirstName = p.FirstName,
                                  LastName = p.LastName,
                                  ExpiryUtc = DateTime.UtcNow.ToLocalTime().ToString("MM/dd/yyyy"),
                                  LicenseNumber = p.Licenses.FirstOrDefault().LicenseNumber,
                                  Quantity = ot.Quantity,
                                  TotalAmount = ot.TotalAmount,
                                  PersonType = (Convert.ToInt32(p.PersonType) != 1) ? "Patient" : "Caregiver"
                              })
                         .OrderByDescending(o => o.OrderDate)
                         .ToListAsync();

            var count = list.Where(p => p.LicenseNumber == model.LicenseNumber).Select(
                p => new
                {
                    p.Quantity,
                    p.LicenseNumber,
                    p.FirstName,
                    p.LastName
                }
                ).Sum(p => p.Quantity);

            if (count + model.Quantity > 12)
                return Ok(count);

            return Ok();
        }

        [Route("Search")]
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,StateProcessor,KindProcessor")]
        public async Task<IActionResult> SearchApplicationList([FromBody]SearchApplicationListViewModel model)
        {
            var list = await context.People.Include(p => p.Licenses).Where(
                p => p.State == TrackableEntityState.IsActive &&
                (
                    p.CompanyName.Contains(model.SearchText) ||
                    p.FirstName.Contains(model.SearchText) ||
                    p.LastName.Contains(model.SearchText) ||
                    p.Licenses.Where(l => l.LicenseNumber.Contains(model.SearchText)).Any() ||
                    p.EmailAddress.Contains(model.SearchText)
                )
            )
            .Select(p => new ApplicationListViewModel
            {
                PersonId = p.PersonId,
                //TODO: Resolve what this date needs to be. 
                ExpiryUtc = DateTime.UtcNow,
                FullName = string.Format("{0}, {1}", p.FirstName, p.LastName),
                CompanyName = p.CompanyName,
                PersonType = p.PersonType,
                EmailAddress = p.EmailAddress,
                LicenseNumber = p.Licenses.FirstOrDefault().LicenseNumber
            })
            .AsNoTracking()
            .ToListAsync();

            return Ok(list);
        }

        [Route("Approve")]
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,StateProcessor")]
        public async Task<IActionResult> Approve([FromBody]ApprovePersonViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Success = false, Message = ModelState });

            if (model.People.Any())
            {
                foreach (var personId in model.People)
                {
                    var person = await context.People.Where(p => p.State == TrackableEntityState.IsActive && p.PersonId == personId).FirstOrDefaultAsync();
                    person.ApplicationStatus = ApplicationStatus.AwaitingPayment;
                    person.ApprovedUtc = DateTime.UtcNow;
                }
            }
            else
            {
                var person = await context.People.Where(p => p.State == TrackableEntityState.IsActive && p.PersonId == model.PersonId).FirstOrDefaultAsync();
                if (null == person)
                    return NotFound(model.PersonId);

                person.ApplicationStatus = ApplicationStatus.AwaitingPayment;
                person.ApprovedUtc = DateTime.UtcNow;
            }

            await context.SaveChangesAsync();

            return Ok(model);
        }

        [Route("ApprovePayment")]
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,StateProcessor")]
        public async Task<IActionResult> ApprovePayment([FromBody]ApprovePersonViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Success = false, Message = ModelState });

            if (model.People.Any())
            {
                foreach (var personId in model.People)
                {
                    var person = await context.People.Where(p => p.State == TrackableEntityState.IsActive && p.PersonId == personId).FirstOrDefaultAsync();
                    person.ApplicationStatus = ApplicationStatus.AwaitingShipment;
                    person.ApprovedUtc = DateTime.UtcNow;
                }
            }
            else
            {
                var person = await context.People.Where(p => p.State == TrackableEntityState.IsActive && p.PersonId == model.PersonId).FirstOrDefaultAsync();
                if (null == person)
                    return NotFound(model.PersonId);

                person.ApplicationStatus = ApplicationStatus.AwaitingShipment;
                person.ApprovedUtc = DateTime.UtcNow;
            }

            await context.SaveChangesAsync();

            return Ok(model);
        }

        [Route("ApproveShipment")]
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,StateProcessor,KindProcessor")]
        public async Task<IActionResult> ApproveShipment([FromBody]ApprovePersonViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Success = false, Message = ModelState });

            if (model.People.Any())
            {
                foreach (var personId in model.People)
                {
                    var person = await context.People.Where(p => p.State == TrackableEntityState.IsActive && p.PersonId == personId).FirstOrDefaultAsync();
                    person.ApplicationStatus = ApplicationStatus.AwaitingPickup;
                    person.ApprovedUtc = DateTime.UtcNow;
                }
            }
            else
            {
                var person = await context.People.Where(p => p.State == TrackableEntityState.IsActive && p.PersonId == model.PersonId).FirstOrDefaultAsync();
                if (null == person)
                    return NotFound(model.PersonId);

                person.ApplicationStatus = ApplicationStatus.AwaitingPickup;
                person.ApprovedUtc = DateTime.UtcNow;
            }

            await context.SaveChangesAsync();

            return Ok(model);
        }

        [Route("Deliverd")]
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,StateProcessor,KindProcessor")]
        public async Task<IActionResult> Deliverd([FromBody]ApprovePersonViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Success = false, Message = ModelState });

            if (model.People.Any())
            {
                foreach (var personId in model.People)
                {
                    var person = await context.People.Where(p => p.State == TrackableEntityState.IsActive && p.PersonId == personId).FirstOrDefaultAsync();
                    person.ApplicationStatus = ApplicationStatus.Complete;
                    person.ApprovedUtc = DateTime.UtcNow;
                }
            }
            else
            {
                var person = await context.People.Where(p => p.State == TrackableEntityState.IsActive && p.PersonId == model.PersonId).FirstOrDefaultAsync();
                if (null == person)
                    return NotFound(model.PersonId);

                person.ApplicationStatus = ApplicationStatus.Complete;
                person.ApprovedUtc = DateTime.UtcNow;
            }

            await context.SaveChangesAsync();

            return Ok(model);
        }

        [Route("Deny")]
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,StateProcessor,KindProcessor")]
        public async Task<IActionResult> Deny([FromBody]DenyPersonViewModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(new { Success = false, Message = ModelState });

            if (model.People.Any())
            {
                foreach (var personId in model.People)
                {
                    var person = await context.People.Where(p => p.State == TrackableEntityState.IsActive && p.PersonId == personId).FirstOrDefaultAsync();
                    person.ApplicationStatus = ApplicationStatus.Denied;
                    person.DeniedReason = model.Reason;
                    person.ApprovedUtc = DateTime.UtcNow;
                }
            }
            else
            {
                var person = await context.People.Where(p => p.State == TrackableEntityState.IsActive && p.PersonId == model.PersonId).FirstOrDefaultAsync();
                if (null == person)
                    return NotFound(model.PersonId);

                person.ApplicationStatus = ApplicationStatus.Denied;
                person.DeniedReason = model.Reason;
                person.ApprovedUtc = DateTime.UtcNow;
            }

            //TODO: This will also need to apply a refund of the amount that they paid since we're doing payment before approval/denial. 
            await context.SaveChangesAsync();

            return Ok(model);
        }

        //    public async Task<IActionResult> GetPeopleList()
        //    {
        //        var list = await context.People.Where(p => p.State == TrackableEntityState.IsActive).AsNoTracking().ToListAsync(); 


        //        pto.PlantTagOrderId as OrderId, 
        //datediff(day, pto.OrderDateUtc, getdate()) as numDays,
        //p.FirstName, 
        //p.LastName,
        //convert(varchar, pto.OrderDateUtc, 1) as OrderDate,
        //oi.Quantity, 
        //p.PersonType
        //    }

        public async Task<IActionResult> FindPerson([FromBody] FindPersonViewModel model)
        {
            var person = await context.People.Where(p => p.FirstName == model.FirstName && p.LastName == model.LastName).FirstOrDefaultAsync();
            return Ok(person.PersonId);
        }
    }
}