import { Component } from '@angular/core';
import { EmployeeUploadComponent } from './employee-upload/employee-upload.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [EmployeeUploadComponent],
  template: `<app-employee-upload></app-employee-upload>`,
})
export class AppComponent { }