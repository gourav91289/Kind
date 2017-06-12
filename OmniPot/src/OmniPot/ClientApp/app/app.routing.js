System.register(["@angular/router", "./components/login/login.component", "./components/admin/users/registration.component"], function (exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var router_1, login_component_1, registration_component_1, routes, AppRoutingProviders, AppRouting;
    return {
        setters: [
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (login_component_1_1) {
                login_component_1 = login_component_1_1;
            },
            function (registration_component_1_1) {
                registration_component_1 = registration_component_1_1;
            }
        ],
        execute: function () {
            routes = [
                {
                    path: '',
                    redirectTo: '',
                    pathMatch: 'full'
                },
                {
                    path: 'login',
                    component: login_component_1.LoginComponent
                },
                {
                    path: 'registration',
                    component: registration_component_1.RegistrationComponent
                }
                //{
                //    path: 'lounge/:id',
                //    component: LoungeDetailComponent
                //},
                //{
                //    path: 'explore/:id',
                //    component: PlaceDetailComponent
                //}
            ];
            exports_1("AppRoutingProviders", AppRoutingProviders = []);
            exports_1("AppRouting", AppRouting = router_1.RouterModule.forRoot(routes));
        }
    };
});
//# sourceMappingURL=app.routing.js.map