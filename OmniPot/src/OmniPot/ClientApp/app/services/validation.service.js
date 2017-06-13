System.register([], function (exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var ValidationService;
    return {
        setters: [],
        execute: function () {
            ValidationService = (function () {
                function ValidationService() {
                }
                ValidationService.getValidatorErrorMessage = function (validatorName, validatorValue) {
                    var config = {
                        'required': '',
                        'invalidPasswordBlank': 'Please enter password',
                        'invalidEmailAddress': 'Please enter valid email address',
                        'invalidPassword': "Invalid password. Password must be at least 8 characters long and more",
                        'minlength': "Minimum length " + validatorValue.requiredLength,
                    };
                    return config[validatorName];
                };
                ValidationService.emailValidator = function (control) {
                    // RFC 2822 compliant regex
                    if (control.value.match(/[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/)) {
                        return null;
                    }
                    else {
                        return { 'invalidEmailAddress': true };
                    }
                };
                ValidationService.passwordValidator = function (control) {
                    // {8,100}           - Assert password is between 8 and 100 characters
                    // (?=.*[0-9])       - Assert a string has at least one number
                    //if (control.value.match(/^(?=.*[0-9])[a-zA-Z0-9!@#$%^&*]{8,100}$/)) {
                    if (control.value.match(/^[a-zA-Z0-9!@#$%^&*]{8,100}$/)) {
                        return null;
                    }
                    else {
                        return { 'invalidPassword': true };
                    }
                };
                return ValidationService;
            }());
            exports_1("ValidationService", ValidationService);
        }
    };
});
//# sourceMappingURL=validation.service.js.map