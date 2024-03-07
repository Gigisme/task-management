import {Component, Inject, Input} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import { Task } from '../../models/task/task';
import {MatButtonModule} from "@angular/material/button";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";

@Component({
  selector: 'app-update-task',
  standalone: true,
    imports: [CommonModule, FormsModule, ReactiveFormsModule, MatButtonModule, MatFormFieldModule, MatInputModule],
  templateUrl: './update-task.component.html',
  styleUrl: './update-task.component.css'
})
export class UpdateTaskComponent {
    constructor(public dialogRef: MatDialogRef<UpdateTaskComponent>, @Inject(MAT_DIALOG_DATA) public data: Task ) {
    }

    form = new FormGroup({
        name: new FormControl(this.data.name, [Validators.required]),
        description: new FormControl(this.data.description, [Validators.required]),
    })

    onSubmit() {
        if (this.form.invalid) {
            return;
        }
        const task: Task = {id: this.data.id, name: this.form.value.name!, description: this.form.value.description!, status: this.data.status}
        this.dialogRef.close(task)
    }
}
