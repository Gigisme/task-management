import {Injectable} from '@angular/core';
import {LoginResponse} from "../models/user/login-response";

@Injectable({
    providedIn: 'root'
})
export class CurrentUserService {

    constructor() {
    }

    saveAuth(response: LoginResponse) {
        localStorage.setItem('username', response.username);
        localStorage.setItem('token', response.token);
    }

    removeAuth() {
        localStorage.removeItem('username');
        localStorage.removeItem('token');
    }

}
