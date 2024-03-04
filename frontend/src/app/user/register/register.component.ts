import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {HttpClient} from "@angular/common/http";
import {CurrentUserService} from "../current-user.service";
import {RegisterRequest} from "../../models/user/register-request";
import {LoginResponse} from "../../models/user/login-response";
import {Router} from "@angular/router";

@Component({
    selector: 'app-register',
    standalone: true,
    imports: [CommonModule, FormsModule, ReactiveFormsModule, MatButtonModule, MatFormFieldModule, MatInputModule],
    templateUrl: './register.component.html',
    styleUrl: './register.component.css'
})
export class RegisterComponent {

    constructor(private http: HttpClient, private currentUser: CurrentUserService, private router: Router) {
    }

    form = new FormGroup({
        username: new FormControl('', [Validators.required]),
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [Validators.required]),
    })

    onSubmit() {
        if (this.form.invalid) { return; }
        let request: RegisterRequest =
            {username: this.form.value.username!, email: this.form.value.email!, password: this.form.value.password!};
        this.registerCall(request).subscribe({
            next: value => {
                this.currentUser.saveAuth(value);
                this.router.navigate(["/home"]);
            },
            error: err => {
                console.log(err);
            },
        })
    }

    registerCall(request : RegisterRequest) {
        return this.http.post<LoginResponse>("http://localhost:5262/api/register", request);
    }
}
