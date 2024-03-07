import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {Task} from "../../models/task/task";
import {CreateTaskComponent} from "../create-task/create-task.component";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
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
import {PatchRequest} from "../../models/task/patch-request";
import {MatButtonModule} from "@angular/material/button";
import {CreateRequest} from "../../models/task/create-request";
import {UpdateTaskComponent} from "../update-task/update-task.component";

@Component({
    selector: 'app-task-list',
    standalone: true,
    imports: [CommonModule, CreateTaskComponent, CdkDropList, CdkDropListGroup, CdkDrag, MatCardModule, MatButtonModule],
    templateUrl: './task-list.component.html',
    styleUrl: './task-list.component.css'
})
export class TaskListComponent implements OnInit {
    toDo: Task[] = [];
    doing: Task[] = [];
    done: Task[] = [];

    headers = new HttpHeaders({
        'Authorization': `Bearer ${this.currentUserService.currentUser$.value?.token}`
    })

    constructor(public dialog: MatDialog, private http: HttpClient, private currentUserService: CurrentUserService) {
    }

    ngOnInit(): void {
        const token = this.currentUserService.currentUser$.value?.token;
        const headers = this.headers;
        this.http.get<Task[]>("http://localhost:5262/api/task/all", {headers}).subscribe({
            next: value => {
                this.assignTasks(value)
            },
            error: err => {
                console.log(err)
            },
        })
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
                let description = result.description;
                if (typeof name === 'string' && typeof description === 'string') {
                    let task: CreateRequest = {name, description}
                    this.createTaskCall(task).subscribe({
                        next: value => {
                            this.toDo.push(value)
                        },
                        error: err => {
                            console.log(err)
                        },
                    })
                }
            }
        })
    }

    openUpdateTaskDialog(task: Task): void {
        const dialogRef: MatDialogRef<UpdateTaskComponent> = this.dialog.open(
            UpdateTaskComponent,
            {
                width: '600px',
                height: '400px',
                data: task,
            }
        )

        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.updateTaskCall(result).subscribe({
                    next: value => {
                        this.editTask(value)
                        console.log(task)
                    },
                    error: err => {
                        console.log(err)
                    },
                })
            }
        })
    }

    drop(event: CdkDragDrop<Task[]>): void {
        // console.log('drop', event);
        if (event.previousContainer === event.container) {
            moveItemInArray(event.container.data, event.previousIndex, event.currentIndex)
        } else {
            const task = event.previousContainer.data[event.previousIndex]
            const status = event.container.id.charAt(event.container.id.length - 1)
            const request: PatchRequest = {id: parseInt(task.id), status: parseInt(status)}
            transferArrayItem(
                event.previousContainer.data,
                event.container.data,
                event.previousIndex,
                event.currentIndex);
            this.moveTaskCall(request).subscribe({
                next: value => {
                    task.status = parseInt(status)
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

    updateTaskCall(task: Task) {
        const headers = this.headers;
        return this.http.put<Task>("http://localhost:5262/api/task/update", task, {headers})
    }

    createTaskCall(task: CreateRequest) {
        const headers = this.headers;
        return this.http.post<Task>("http://localhost:5262/api/task/create", task,{headers})
    }

    moveTaskCall(request: PatchRequest) {
        const headers = this.headers;
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

    editTask(task: Task) {
        let index = 0
        switch (task.status) {
            case 0:
                index = this.toDo.findIndex(t => t.id == task.id);
                this.toDo[index] = task
                break;
            case 1:
                index = this.doing.findIndex(t => t.id == task.id);
                this.doing[index] = task
                break
            case 2:
                index = this.done.findIndex(t => t.id == task.id);
                this.done[index] = task
                break;
        }
    }

    deleteCall(task: Task) {
        const headers = this.headers;
        const taskId = parseInt(task.id)
        this.http.delete(`http://localhost:5262/api/task/delete?id=${taskId}`, {headers}).subscribe({
            next: value => {
                console.log(task)
                this.removeTask(task)
            },
            error: err => {
                console.log(err)
            },
        })
    }
}
