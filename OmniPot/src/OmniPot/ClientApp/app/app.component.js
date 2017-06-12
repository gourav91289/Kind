System.register(["@angular/core", "@angular/platform-browser", "@angular/router", "@angular/http", "./security/auth.service"], function (exports_1, context_1) {
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
    var core_1, platform_browser_1, router_1, http_1, auth_service_1, AppComponent;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
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
            function (auth_service_1_1) {
                auth_service_1 = auth_service_1_1;
            }
        ],
        execute: function () {
            AppComponent = (function () {
                function AppComponent(router, titleService, http, authService) {
                    this.router = router;
                    this.titleService = titleService;
                    this.http = http;
                    this.authService = authService;
                    this.title = "Agrisoft";
                }
                AppComponent.prototype.ngOnInit = function () {
                    //setting title
                    this.setTitle('Agrisoft');
                    // subscribe to form changes  
                    this.isLoggedIn();
                };
                // wrapper to the Angular title service.
                AppComponent.prototype.setTitle = function (newTitle) {
                    this.titleService.setTitle(newTitle);
                };
                // provide local page the user's logged in status (do we have a token or not)
                AppComponent.prototype.isLoggedIn = function () {
                    var val = this.authService.loggedIn();
                    if (!this.authService.loggedIn()) {
                        this.router.navigate(['/login']);
                    }
                };
                return AppComponent;
            }());
            AppComponent = __decorate([
                core_1.Component({
                    selector: "app-root",
                    templateUrl: './template/app.component.html'
                }),
                __metadata("design:paramtypes", [router_1.Router, platform_browser_1.Title, http_1.Http, auth_service_1.AuthService])
            ], AppComponent);
            exports_1("AppComponent", AppComponent);
        }
    };
});
//# sourceMappingURL=app.component.js.map