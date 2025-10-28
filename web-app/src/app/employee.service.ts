import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeePairResult } from './employee-upload/employee-upload.component';
import { environment } from '../environments/environment';

@Injectable({ providedIn: 'root' })
export class EmployeeService {
  private apiUrl = `${environment.apiBaseUrl}/employees/upload`;

  constructor(private http: HttpClient) {}

  uploadCsv(fileData: FormData): Observable<EmployeePairResult[]> {
    return this.http.post<EmployeePairResult[]>(this.apiUrl, fileData);
  }
}
