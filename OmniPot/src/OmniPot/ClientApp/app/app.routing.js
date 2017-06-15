System.register(["@angular/router", "./security/auth-guard.service", "./components/login/login.component", "./components/dashboard/dashboard.component", "./components/token.verify.component"], function (exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var router_1, auth_guard_service_1, login_component_1, dashboard_component_1, token_verify_component_1, routes, AppRoutingProviders, AppRouting;
    return {
        setters: [
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (auth_guard_service_1_1) {
                auth_guard_service_1 = auth_guard_service_1_1;
            },
            function (login_component_1_1) {
                login_component_1 = login_component_1_1;
            },
            function (dashboard_component_1_1) {
                dashboard_component_1 = dashboard_component_1_1;
            },
            function (token_verify_component_1_1) {
                token_verify_component_1 = token_verify_component_1_1;
            }
        ],
        execute: function () {
            routes = [
                // Verifying token
                { path: '', component: token_verify_component_1.TokenVerifyComponent, canActivate: [auth_guard_service_1.AuthGuard] },
                // App views
                { path: 'login', component: login_component_1.LoginComponent },
                { path: 'dashboard', component: dashboard_component_1.DashboardComponent, canActivate: [auth_guard_service_1.AuthGuard] },
            ];
            exports_1("AppRoutingProviders", AppRoutingProviders = []);
            exports_1("AppRouting", AppRouting = router_1.RouterModule.forRoot(routes));
        }
    };
});
//# sourceMappingURL=app.routing.js.map