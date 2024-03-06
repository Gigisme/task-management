import {Injectable} from '@angular/core';
import {LoginResponse} from "../models/user/login-response";
import {BehaviorSubject} from "rxjs";
import {User} from "../models/user/user";
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Injectable({
    providedIn: 'root'
})
export class CurrentUserService {

    currentUser$ = new BehaviorSubject<User | null | undefined>(undefined)

    constructor(private http: HttpClient) {
    }

    setCurrentUser() {
        const token = localStorage.getItem('token');
        if (token)
        {
            const headers = new HttpHeaders({
                'Authorization': `Bearer ${token}`
            })
            this.http.get<LoginResponse>("http://localhost:5262/api/user", { headers }).subscribe({
                next: value => {
                    this.saveAuth(value);
                },
                error: err => {
                    if (err.status == 401)
                    {
                        this.currentUser$.next(null);
                    } else {
                        console.log(err)
                    }
                },
            })
        } else {
            this.currentUser$.next(null);
        }
    }

    saveAuth(response: LoginResponse) {
        localStorage.setItem('token', response.token);
        this.currentUser$.next({username: response.username, token: response.token});
    }

    removeAuth() {
        localStorage.removeItem('token');
        this.currentUser$.next(null);
    }

}
