export class ValidationService {

    static getValidatorErrorMessage(validatorName: string, validatorValue?: any) {
        let config = {
            'required': '',
            'invalidPasswordBlank': 'Please enter password',
            'invalidEmailAddress': 'Please enter valid email address',
            'invalidPassword': `Invalid password. Password must be at least 8 characters long and more`,
            'minlength': `Minimum length ${validatorValue.requiredLength}`,            
        };

        return config[validatorName];
    }

    static emailValidator(control) {
        // RFC 2822 compliant regex
        if (control.value.match(/[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/)) {
            return null;
        } else {
            return { 'invalidEmailAddress': true };
        }
    }    

    static passwordValidator(control) {
        // {8,100}           - Assert password is between 8 and 100 characters
        // (?=.*[0-9])       - Assert a string has at least one number
        //if (control.value.match(/^(?=.*[0-9])[a-zA-Z0-9!@#$%^&*]{8,100}$/)) {
        if (control.value.match(/^[a-zA-Z0-9!@#$%^&*]{8,100}$/)) {
            return null;
        } else {
            return { 'invalidPassword': true };
        }
    }
}
