import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatDialogRef} from "@angular/material/dialog";
import {FormBuilder, FormsModule, ReactiveFormsModule} from "@angular/forms";

@Component({
    selector: 'app-create-task',
    standalone: true,
    imports: [CommonModule, FormsModule, ReactiveFormsModule],
    templateUrl: './create-task.component.html',
    styleUrl: './create-task.component.css'
})
export class CreateTaskComponent {
    constructor(public dialogRef: MatDialogRef<CreateTaskComponent>, private fb: FormBuilder) {
    }

    taskForm = this.fb.group({
        name: '',
        description: '',
        priority: 1,
    });

    onSubmit() {
        this.dialogRef.close(this.taskForm.value)
    }
}
