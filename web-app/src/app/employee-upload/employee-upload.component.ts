import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EmployeeService } from '../employee.service';

export interface EmployeePairResult {
  employeeId1: number;
  employeeId2: number;
  projectId: number;
  totalDaysWorked: number;
}

export interface ProblemDetails {
  type?: string;
  title: string;
  status?: number;
  detail?: string;
  instance?: string;
}

@Component({
  selector: 'app-employee-upload',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="container py-4">
      <h2 class="mb-4">Employee Pair CSV Upload</h2>

      <div class="mb-3">
        <input
          type="file"
          class="form-control"
          (change)="onFileSelected($event)"
          accept=".csv"
        />
      </div>

      <div class="mb-3">
        <button class="btn btn-primary" (click)="upload()" [disabled]="loading">
          {{ loading ? 'Uploading...' : 'Upload' }}
        </button>
      </div>

      <div *ngIf="error" class="alert alert-danger">
        {{ error }}
      </div>

      <table *ngIf="results.length > 0" class="table table-striped mt-3">
        <thead class="table-dark">
          <tr>
            <th>Employee ID #1</th>
            <th>Employee ID #2</th>
            <th>Project ID</th>
            <th>Days Worked</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let item of results">
            <td>{{ item.employeeId1 }}</td>
            <td>{{ item.employeeId2 }}</td>
            <td>{{ item.projectId }}</td>
            <td>{{ item.totalDaysWorked }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  `,
})
export class EmployeeUploadComponent {
  selectedFile: File | null = null;
  results: EmployeePairResult[] = [];
  loading = false;
  error = '';

  constructor(private employeeService: EmployeeService) {}

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    this.selectedFile = input.files?.[0] ?? null;

    this.results = [];
    this.error = '';
  }

  upload() {
    if (!this.selectedFile) {
      this.error = 'Please select a CSV file.';
      return;
    }

    const formData = new FormData();
    formData.append('file', this.selectedFile, this.selectedFile.name);

    this.loading = true;
    this.error = '';

    this.employeeService.uploadCsv(formData).subscribe({
      next: (data: EmployeePairResult[]) => {
        this.results = data;
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;

        if (err.error?.title) {
          const problem = err.error as ProblemDetails;
          this.error = `${problem.title}${problem.detail ? ': ' + problem.detail : ''}`;
        } else {
          this.error = 'Failed to upload or parse CSV.';
        }

        console.error('Upload error:', err);
      },
    });
  }
}
