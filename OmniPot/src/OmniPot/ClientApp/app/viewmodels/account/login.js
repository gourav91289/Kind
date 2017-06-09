System.register([], function (exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var LoginViewModel;
    return {
        setters: [],
        execute: function () {
            LoginViewModel = (function () {
                function LoginViewModel(username, password) {
                    this.username = username;
                    this.password = password;
                }
                return LoginViewModel;
            }());
            exports_1("LoginViewModel", LoginViewModel);
        }
    };
});
//# sourceMappingURL=login.js.map