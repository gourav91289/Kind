System.register(["@angular/core", "@angular/forms"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var __moduleName = context_1 && context_1.id;
    var core_1, forms_1, RegistrationComponent;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (forms_1_1) {
                forms_1 = forms_1_1;
            }
        ],
        execute: function () {
            RegistrationComponent = (function () {
                function RegistrationComponent(_fb) {
                    this._fb = _fb;
                    this.events = [];
                }
                RegistrationComponent.prototype.ngOnInit = function () {
                    this.myForm = this._fb.group({
                        name: ['', [forms_1.Validators.required, forms_1.Validators.minLength(5)]],
                        address: this._fb.group({
                            street: ['', forms_1.Validators.required],
                            postcode: ['8000']
                        })
                    });
                    // subscribe to form changes 
                    this.subcribeToFormChanges();
                    // Update single value
                    this.myForm.controls['name']
                        .setValue('John', { onlySelf: true });
                    // Update form model
                    // const people = {
                    // 	name: 'Jane',
                    // 	address: {
                    // 		street: 'High street',
                    // 		postcode: '94043'
                    // 	}
                    // };
                    // (<FormGroup>this.myForm)
                    //     .setValue(people, { onlySelf: true });
                };
                RegistrationComponent.prototype.subcribeToFormChanges = function () {
                    var _this = this;
                    var myFormStatusChanges$ = this.myForm.statusChanges;
                    var myFormValueChanges$ = this.myForm.valueChanges;
                    myFormStatusChanges$.subscribe(function (x) { return _this.events.push({ event: 'STATUS_CHANGED', object: x }); });
                    myFormValueChanges$.subscribe(function (x) { return _this.events.push({ event: 'VALUE_CHANGED', object: x }); });
                };
                RegistrationComponent.prototype.save = function (model, isValid) {
                    this.submitted = true;
                    console.log(model, isValid);
                };
                return RegistrationComponent;
            }());
            RegistrationComponent = __decorate([
                core_1.Component({
                    //moduleId: module.id,
                    selector: "userreg",
                    templateUrl: './template/admin/users/registration.component.html',
                }),
                __metadata("design:paramtypes", [forms_1.FormBuilder])
            ], RegistrationComponent);
            exports_1("RegistrationComponent", RegistrationComponent);
        }
    };
});
//# sourceMappingURL=registration.component.js.map