import { EmployeeDataService } from "./../services/employee-data.service";
import { Component, Inject } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { FormControl, Validators } from "@angular/forms";
import { Employee } from "../model/employee";
import * as moment from "moment";

@Component({
  selector: "app-employee-edit",
  templateUrl: "./employee-edit.component.html",
  styleUrls: ["./employee-edit.component.css"],
})
export class EmployeeEditComponent {
  constructor(
    public dialogRef: MatDialogRef<EmployeeEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Employee,
    private employeeDataService: EmployeeDataService
  ) {}
  // date mask
  datemask = [/\d/, /\d/, "/", /\d/, /\d/, "/", /\d/, /\d/, /\d/, /\d/];
  employeeForm = new FormControl("", [Validators.required]);
  // .setErrors({ 'invalid': false });
  onNoClick(): void {
    this.dialogRef.close();
  }

  updateEmployee(): void {
    // save data to Server
   this.data.dateOfBirth = this.convertToDate(this.data.dateOfBirth);
   this.data.startDate = this.convertToDate(this.data.startDate);
    this.employeeDataService
      .updateEmployee(this.data)
      .subscribe((data) => {
        this.data = data;
      });
  }
  convertToDate(date: Date) {
    // Check if date is ISO 8601
    if(moment(date.toString(), moment.ISO_8601).isValid()) {
      return date;
    }
    else {
      return moment(date.toString(), "dd/MM/yyyy").toDate();
    }
  }
}
