import {Component, OnInit} from '@angular/core';
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
import {MatCardModule} from "@angular/material/card";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {CurrentUserService} from "../../user/current-user.service";
import {PatchRequest} from "../../models/user/patch-request";

@Component({
    selector: 'app-task-list',
    standalone: true,
    imports: [CommonModule, CreateTaskComponent, CdkDropList, CdkDropListGroup, CdkDrag, MatCardModule],
    templateUrl: './task-list.component.html',
    styleUrl: './task-list.component.css'
})
export class TaskListComponent implements OnInit {
    toDo: Task[] = [];
    doing: Task[] = [];
    done: Task[] = [];

    constructor(public dialog: MatDialog, private http: HttpClient, private currentUserService: CurrentUserService) {
    }

    ngOnInit(): void {
        const token = this.currentUserService.currentUser$.value?.token;
        const headers = new HttpHeaders({
            'Authorization': `Bearer ${token}`
        })
        this.http.get<Task[]>("http://localhost:5262/api/task/all", {headers}).subscribe({
            next: value => {
                this.assignTasks(value)
            },
            error: err => {
                console.log(err)
            },
        })
    }

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
            const status = event.container.id.charAt(event.container.id.length - 1)
            const userTaskId = event.previousContainer.data[event.previousIndex].id
            const request: PatchRequest = {id: parseInt(userTaskId), status: parseInt(status)}
            transferArrayItem(
                event.previousContainer.data,
                event.container.data,
                event.previousIndex,
                event.currentIndex);
            this.moveTaskCall(request).subscribe({
                next: value => {

                },
                error: err => {
                    console.log(err)
                    //Transfer back if failed
                    transferArrayItem(
                        event.container.data,
                        event.previousContainer.data,
                        event.currentIndex,
                        event.previousIndex);
                },
            })
        }
    }

    moveTaskCall(request: PatchRequest) {
        const headers = new HttpHeaders({
            'Authorization': `Bearer ${this.currentUserService.currentUser$.value?.token}`
        })
        return this.http.patch("http://localhost:5262/api/task/patch", request, {headers})
    }

    assignTasks(tasks: Task[]) {
        tasks.map(task => {
            switch (task.status) {
                case 0:
                    this.toDo.push(task)
                    break;
                case 1:
                    this.doing.push(task)
                    break
                case 2:
                    this.done.push(task)
                    break;
            }
        })
    }

    removeTask(task: Task) {
        switch (task.status) {
            case 0:
                this.toDo = this.toDo.filter(t => t.id != task.id)
                break;
            case 1:
                this.doing = this.doing.filter(t => t.id != task.id)
                break
            case 2:
                this.done = this.done.filter(t => t.id != task.id)
                break;
        }
    }

    deleteCall(task: Task) {
        const headers = new HttpHeaders({
            'Authorization': `Bearer ${this.currentUserService.currentUser$.value?.token}`
        })
        const taskId = parseInt(task.id)
        this.http.delete(`http://localhost:5262/api/task/delete?id=${taskId}`, {headers}).subscribe({
            next: value => {
                this.removeTask(task)
            },
            error: err => {
                console.log(err)
            },
        })
    }
}
