import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {TaskListComponent} from "../tasks/task-list/task-list.component";
import {RouterOutlet} from "@angular/router";
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatIconModule} from "@angular/material/icon";
import {MatButtonModule} from "@angular/material/button";

@Component({
    selector: 'app-home',
    standalone: true,
    imports: [CommonModule, RouterOutlet, TaskListComponent, MatToolbarModule, MatIconModule, MatButtonModule],
    templateUrl: './home.component.html',
    styleUrl: './home.component.css'
})
export class HomeComponent {

}
