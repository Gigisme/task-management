import {FormGroup} from "@angular/forms";

export class ErrorMessageManager {
    errorMessages: { [key: string]: string } = {
        required: 'This field is required.',
        minlength: 'Minimum length is ',
        maxlength: 'Maximum length is 20 characters.',
        invalidUsername: 'Invalid username.',
        invalidPassword: 'Invalid password.',
        email: 'Email address is not valid.',
        takenUsername: 'Username is taken.',
        takenEmail: 'Email is taken.',
    };

    constructor(private form: FormGroup) {
    }

    get(controlName: string): string | null {
        const control = this.form.get(controlName);
        if (control?.errors) {
            const errorKey = Object.keys(control.errors)[0];
            const errorMessage = this.errorMessages[errorKey];
            if (errorKey === 'minlength') {
                const minLength = control.errors?.['minlength']?.requiredLength;
                return `${errorMessage} ${minLength}.`;
            }
            return this.errorMessages[errorKey];
        }
        return null;
    }
}
