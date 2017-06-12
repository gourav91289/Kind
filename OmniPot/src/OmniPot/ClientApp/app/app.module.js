System.register(["@angular/core", "@angular/platform-browser", "./app.routing", "@angular/common", "@angular/forms", "@angular/http", "rxjs/Rx", "./app.component", "./components/control-messages.component", "./components/login/login.component", "./services/validation.service", "./security/auth.service", "./security/auth-guard.service"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __moduleName = context_1 && context_1.id;
    var core_1, platform_browser_1, app_routing_1, common_1, forms_1, http_1, app_component_1, control_messages_component_1, login_component_1, validation_service_1, auth_service_1, auth_guard_service_1, AppModule;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (platform_browser_1_1) {
                platform_browser_1 = platform_browser_1_1;
            },
            function (app_routing_1_1) {
                app_routing_1 = app_routing_1_1;
            },
            function (common_1_1) {
                common_1 = common_1_1;
            },
            function (forms_1_1) {
                forms_1 = forms_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (_1) {
            },
            function (app_component_1_1) {
                app_component_1 = app_component_1_1;
            },
            function (control_messages_component_1_1) {
                control_messages_component_1 = control_messages_component_1_1;
            },
            function (login_component_1_1) {
                login_component_1 = login_component_1_1;
            },
            function (validation_service_1_1) {
                validation_service_1 = validation_service_1_1;
            },
            function (auth_service_1_1) {
                auth_service_1 = auth_service_1_1;
            },
            function (auth_guard_service_1_1) {
                auth_guard_service_1 = auth_guard_service_1_1;
            }
        ],
        execute: function () {
            AppModule = (function () {
                function AppModule() {
                }
                return AppModule;
            }());
            AppModule = __decorate([
                core_1.NgModule({
                    // modules
                    imports: [platform_browser_1.BrowserModule, forms_1.FormsModule, forms_1.ReactiveFormsModule, http_1.HttpModule, app_routing_1.AppRouting],
                    // directives, components, and pipes
                    declarations: [control_messages_component_1.ControlMessagesComponent, app_component_1.AppComponent, login_component_1.LoginComponent],
                    // providers
                    providers: [validation_service_1.ValidationService, auth_service_1.AuthService, auth_guard_service_1.AuthGuard, platform_browser_1.Title, { provide: common_1.APP_BASE_HREF, useValue: '/' }],
                    bootstrap: [app_component_1.AppComponent]
                })
            ], AppModule);
            exports_1("AppModule", AppModule);
        }
    };
});
//# sourceMappingURL=app.module.js.map