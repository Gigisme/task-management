import {Component, Input} from '@angular/core';
import { CommonModule } from '@angular/common';
import { Task} from "../models/task";
import {HomeComponent} from "../home/home.component";
import {CreateTaskComponent} from "../create-task/create-task.component";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {v4} from "uuid";

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, HomeComponent, CreateTaskComponent],
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.css'
})
export class TaskListComponent {
  constructor(public dialog: MatDialog) {
  }
  tasks: Task[] = [];

  @Input() name: string | undefined;
  addTask(task: Task): void {
    this.tasks.push(task);
  }

  openCreateTaskDialog(): void {
    const dialogRef: MatDialogRef<CreateTaskComponent> = this.dialog.open(
      CreateTaskComponent,
      {
        width: '600px',
        height: '400px',
      }
    )

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        let name = result.name;
        let desc = result.description;
        if (typeof name ==='string' && typeof desc ==='string') {
          let task = new Task(v4(), name, desc, name)
          console.log('Data recieved: ',result);
          this.addTask(result);
        }

      }
    })
  }


}
