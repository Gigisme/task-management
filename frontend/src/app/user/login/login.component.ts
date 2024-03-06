import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatCardModule} from "@angular/material/card";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatButtonModule} from "@angular/material/button";
import {HttpClient} from "@angular/common/http";
import {LoginResponse} from "../../models/user/login-response";
import {LoginRequest} from "../../models/user/login-request";
import {CurrentUserService} from "../current-user.service";
import {Router} from "@angular/router";
import {ErrorMessageManager} from "../../errors/errorMessageManager";

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule],
    templateUrl: './login.component.html',
    styleUrl: './login.component.css'
})
export class LoginComponent {

    errorMessageManager: ErrorMessageManager;

    constructor(private http: HttpClient, private currentUser: CurrentUserService, private router: Router) {
        this.errorMessageManager = new ErrorMessageManager(this.form)
    }

    form = new FormGroup({
        username: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]),
        password: new FormControl('', [Validators.required, Validators.minLength(7), Validators.maxLength(20)]),
    })

    onSubmit() {
        if (this.form.invalid) {
            return;
        }
        let request: LoginRequest = {username: this.form.value.username!, password: this.form.value.password!};
        this.loginCall(request).subscribe({
            next: value => {
                this.currentUser.saveAuth(value);
                this.router.navigate(["/home"]);
            },
            error: err => {
                if (err.status == 401) {
                    const errorMessage = err.error;
                    if (errorMessage === 'Invalid username') {
                        this.form.get('username')?.setErrors({'invalidUsername': true});
                    } else if (errorMessage === 'Invalid password') {
                        this.form.get('password')?.setErrors({'invalidPassword': true});
                    }
                }
            },
        })
    }

    loginCall(request: LoginRequest) {
        return this.http.post<LoginResponse>("http://localhost:5262/api/login", request);
    }
}
