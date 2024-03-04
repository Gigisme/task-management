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

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule],
    templateUrl: './login.component.html',
    styleUrl: './login.component.css'
})
export class LoginComponent {

    constructor(private http: HttpClient, private currentUser: CurrentUserService, private router: Router) {
    }

    form = new FormGroup({
        username: new FormControl('', [Validators.required]),
        password: new FormControl('', [Validators.required]),
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
                console.log(err);
            },
        })
    }

    loginCall(request: LoginRequest) {
        return this.http.post<LoginResponse>("http://localhost:5262/api/login", request);
    }



}
