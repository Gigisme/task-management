import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterOutlet} from '@angular/router';
import {HomeComponent} from "./home/home.component";
import {ReactiveFormsModule} from "@angular/forms";
import {CurrentUserService} from "./user/current-user.service";

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [CommonModule, RouterOutlet, HomeComponent, ReactiveFormsModule],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

    title = 'task-management';

    constructor(private currentUserService: CurrentUserService) {
    }

    ngOnInit(): void {
        this.currentUserService.setCurrentUser();
    }
}
