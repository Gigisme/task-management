import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {Task} from "../../models/task";
import {CreateTaskComponent} from "../create-task/create-task.component";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {v4} from "uuid";
import {
    CdkDrag,
    CdkDragDrop,
    CdkDropList,
    CdkDropListGroup,
    moveItemInArray,
    transferArrayItem
} from "@angular/cdk/drag-drop";

@Component({
    selector: 'app-task-list',
    standalone: true,
    imports: [CommonModule, CreateTaskComponent, CdkDropList, CdkDropListGroup, CdkDrag],
    templateUrl: './task-list.component.html',
    styleUrl: './task-list.component.css'
})
export class TaskListComponent {
    constructor(public dialog: MatDialog) {
    }

    toDo: Task[] = [];
    doing: Task[] = [];
    done: Task[] = [];


    openCreateTaskDialog(taskList: Task[]): void {
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
                if (typeof name === 'string' && typeof desc === 'string') {
                    let task = new Task(v4(), name, desc, result.priority)
                    //console.log('Data recieved: ',task);
                    taskList.push(task);
                }
            }
        })
    }

    drop(event: CdkDragDrop<Task[]>): void {
        // console.log('drop', event);
        if (event.previousContainer === event.container) {
            moveItemInArray(event.container.data, event.previousIndex, event.currentIndex)
        } else {
            transferArrayItem(
                event.previousContainer.data,
                event.container.data,
                event.previousIndex,
                event.currentIndex);
        }
    }


}
