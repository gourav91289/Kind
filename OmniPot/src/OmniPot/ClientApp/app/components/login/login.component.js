System.register(["@angular/core", "@angular/forms", "@angular/platform-browser", "@angular/router", "@angular/http", "../../services/validation.service", "../../security/auth.service"], function (exports_1, context_1) {
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
    var core_1, forms_1, platform_browser_1, router_1, http_1, validation_service_1, auth_service_1, LoginComponent;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (forms_1_1) {
                forms_1 = forms_1_1;
            },
            function (platform_browser_1_1) {
                platform_browser_1 = platform_browser_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (validation_service_1_1) {
                validation_service_1 = validation_service_1_1;
            },
            function (auth_service_1_1) {
                auth_service_1 = auth_service_1_1;
            }
        ],
        execute: function () {
            LoginComponent = (function () {
                function LoginComponent(router, titleService, http, authService, formBuilder) {
                    this.router = router;
                    this.titleService = titleService;
                    this.http = http;
                    this.authService = authService;
                    this.formBuilder = formBuilder;
                    this.events = [];
                    this.logoPath = './images/logo.png';
                }
                LoginComponent.prototype.ngOnInit = function () {
                    this.loginForm = this.formBuilder.group({
                        'email': ['', [forms_1.Validators.required, validation_service_1.ValidationService.emailValidator]],
                        'password': ['', [forms_1.Validators.required, validation_service_1.ValidationService.passwordValidator]],
                        'rememberMe': [true]
                    });
                    //setting title
                    this.setTitle('Agrisoft - Login');
                    // subscribe to form changes  
                    this.subcribeToFormChanges();
                };
                // wrapper to the Angular title service.
                LoginComponent.prototype.setTitle = function (newTitle) {
                    this.titleService.setTitle(newTitle);
                };
                LoginComponent.prototype.subcribeToFormChanges = function () {
                    var _this = this;
                    var myFormStatusChanges$ = this.loginForm.statusChanges;
                    var myFormValueChanges$ = this.loginForm.valueChanges;
                    myFormStatusChanges$.subscribe(function (x) { return _this.events.push({ event: 'STATUS_CHANGED', object: x }); });
                    myFormValueChanges$.subscribe(function (x) { return _this.events.push({ event: 'VALUE_CHANGED', object: x }); });
                };
                // post the user's login details to server, if authenticated token is returned, then token is saved to session storage
                LoginComponent.prototype.login = function (user) {
                    var _this = this;
                    //alert(`Email: ${user.email}, Password: ${user.password}, Remember: ${user.rememberMe}`);
                    this.isLoginError = false;
                    if (this.loginForm.dirty && this.loginForm.valid) {
                        this.http.post('/api/account/authenticate', user, { headers: this.authService.contentHeaders() })
                            .subscribe(function (response) {
                            // success, save the token to session storage
                            var result = response.json();
                            if (result.Succeeded) {
                                _this.authService.login(user, result);
                                _this.router.navigate(['/dashboard']);
                            }
                            else {
                                _this.isLoginError = true;
                                _this.loginErrorMessage = result.Message;
                            }
                        }, function (error) {
                            // failed; TODO: add some nice toast / error handling
                            console.log(error.text());
                        });
                    }
                };
                return LoginComponent;
            }());
            LoginComponent = __decorate([
                core_1.Component({
                    selector: "login",
                    template: "<div class=\"container\">\n    <div class=\"col-lg-5 col-sm-6 col-lg-offset-1 child\">\n        <img [src]=\"logoPath\" style=\"margin:40% 0 0 0\" class=\"img-responsive\" alt=\"AGRSoft\">\n    </div>\n\n    <div class=\"col-lg-4 col-sm-6 col-lg-offset-1 login-font\">\n        <form [formGroup]=\"loginForm\" novalidate (submit)=\"login(loginForm.value)\">\n            <h1 class=\"h1\">Log In</h1>\n            <span class=\"font-size11\">Please enter your username and password to login</span>\n\n            <div class=\"form-group offset-top-3 \">\n                <input type=\"text\" class=\"form-control\" formControlName=\"email\" id=\"email\" placeholder=\"Username\" title=\"Please enter you username\">\n                <control-messages [control]=\"loginForm.controls.email\" class=\"help-block\"></control-messages>\n            </div>\n\n            <div class=\"form-group\">\n                <input type=\"password\" class=\"form-control\" id=\"password\" name=\"password\" formControlName=\"password\" placeholder=\"Password\" title=\"Please enter your password\">\n                <control-messages [control]=\"loginForm.controls.password\" class=\"help-block\"></control-messages>\n            </div>\n            <div id=\"loginErrorMsg\" *ngIf=\"isLoginError\" class=\"alert alert-danger\">{{loginErrorMessage}}</div>\n            <div class=\"checkbox\">\n                <label class=\"font-size11\">\n                    <input type=\"checkbox\" name=\"remember\" id=\"remember\" formControlName=\"rememberMe\">Remember login\n                </label>\n                <a href=\"#\" class=\"pull-right font-size11\">Forgot Password</a>\n            </div>\n            <button type=\"submit\" class=\"btn btn-primary btn-custom offset-top-2 offset-bottom-3\"\n                    [disabled]=\"!loginForm.valid\"><i class=\"fa fa-angle-right\"></i>Log In </button>\n        </form>\n        <hr />\n        <a href=\"#\" class=\"font-size14\">Need help with login?</a><br>\n        <a href=\"#\" class=\"font-size12\"><strong>Click here</strong></a> <span class=\"font-size12\">to read our FAQ section.</span>\n    </div>\n</div>",
                }),
                __metadata("design:paramtypes", [router_1.Router,
                    platform_browser_1.Title,
                    http_1.Http,
                    auth_service_1.AuthService,
                    forms_1.FormBuilder])
            ], LoginComponent);
            exports_1("LoginComponent", LoginComponent);
        }
    };
});
//# sourceMappingURL=login.component.js.map