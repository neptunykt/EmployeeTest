import { Employee } from "./../model/employee";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Paginator } from "./../model/paginator";
import { environment } from "src/environments/environment";
import { OrderDirection } from "../model/order-direction";

@Injectable({
  providedIn: "root",
})
export class EmployeeDataService {
  constructor(private httpClient: HttpClient) {}

  getEmployees(
    pageIndex: number,
    pageSize: number,
    orderBy: string,
    orderDirection: OrderDirection,
    searchText: string,
    searchDate: string
  ): Observable<Paginator<Employee>> {
    let headers = new HttpHeaders({
      "Content-Type": "application/json; charset=utf-8",
    });
    let params = new HttpParams().set("pageSize", pageSize);
    if (orderBy) {
      params = params.set("orderBy", orderBy);
    }
    if (orderDirection) {
      params = params.set("orderDirection", orderDirection);
    }
    if(searchText) {
      params = params.set("searchText", searchText);
    }
    if(searchDate) {
      params = params.set("searchDate", searchDate);
    }
    return this.httpClient.get<Paginator<Employee>>(
      `${environment.api}/api/Employee/page/${pageIndex}`,
      { headers: headers, params: params }
    );
  }

  updateEmployee(employee: Employee): Observable<Employee> {
    return this.httpClient.put<Employee>(
      `${environment.api}/api/Employee/${employee.id}`, employee);
  }
}
