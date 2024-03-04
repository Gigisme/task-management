import {Routes} from '@angular/router';
import {LoginComponent} from "./user/login/login.component";
import {HomeComponent} from "./home/home.component";
import {RegisterComponent} from "./user/register/register.component";

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',
    },
    {
        path: 'login',
        component: LoginComponent,
    },
    {
        path: 'home',
        component: HomeComponent,
    },
    {
        path: 'registration',
        component: RegisterComponent,
    },
];
