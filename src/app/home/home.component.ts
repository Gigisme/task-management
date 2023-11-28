import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {TaskListComponent} from "../task-list/task-list.component";
import {RouterOutlet} from "@angular/router";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterOutlet, TaskListComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
