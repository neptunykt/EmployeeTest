import { ModalMessageComponent } from "./modal-message/modal-message.component";
import { EmployeeDataService } from "./services/employee-data.service";
import { EmployeeEditComponent } from "./employee-edit/employee-edit.component";
import { AfterViewInit, Component, OnInit, ViewChild } from "@angular/core";
import { MatTableDataSource } from "@angular/material/table";
import { MatPaginator } from "@angular/material/paginator";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { MatTable } from "@angular/material/table";
import { MatDialog } from "@angular/material/dialog";
import { MatSort, Sort } from "@angular/material/sort";
import { environment } from "src/environments/environment";
import { Employee } from "src/app/model/employee";
import { FormControl, NgModel } from "@angular/forms";
import { OrderDirection } from "./model/order-direction";


@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
})
export class AppComponent implements OnInit, AfterViewInit {
  fileName = "";
  searchDateTextValue: string = "";
  searchTextValue: string = "";
  employees: Employee[] = [];
  // Order by default surname
  orderBy: string = "surname";
  // Order by default ascending
  orderDirection: OrderDirection = OrderDirection.Asc;
  totalRows = 0;
  pageSize = 5;
  currentPage = 0;
  displayedColumns: string[] = [
    "id",
    "payRollNumber",
    "surname",
    "forenames",
    "dateOfBirth",
    "telephone",
    "mobile",
    "address",
    "address2",
    "postCode",
    "emailHome",
    "startDate",
    "action",
  ];
  pageSizeOptions: number[] = [5, 10, 25, 100];
  datemask = [/\d/, /\d/, "/", /\d/, /\d/, "/", /\d/, /\d/, /\d/, /\d/];
  loading: boolean = true;
  environmentUrl: string = environment.api;
  dataSource = new MatTableDataSource<Employee>();
  employeesSearchForm = new FormControl();
  public rowId: string ="";
  title = "pagination";
  @ViewChild(MatTable, { static: true }) table: MatTable<Employee>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild("matTableSort") matTableSort = new MatSort();
  @ViewChild('searchDate') searchDate: NgModel;

  constructor(
    private http: HttpClient,
    public dialog: MatDialog,
    private employeeDataService: EmployeeDataService
  ) {}

  ngOnInit() {
   this.loadData();
  }

  
 ngAfterViewInit() {
  this.matTableSort.sortChange.subscribe(s=>{
   this.orderBy = s.active;
   this.orderDirection = s.direction === 'asc' ? OrderDirection.Asc : OrderDirection.Desc;
   this.loadData();
  });
  }

  onFileSelected(event) {
    const file: File = event.target.files[0];

    if (file) {
      this.fileName = file.name;
      const formData = new FormData();
      let header = new HttpHeaders();
      formData.append("upload", file, file.name);
      header.append("Content-Type", "multipart/form-data");
      header.append("Accept", "application/json");
      const upload$ = this.http.post(
        `${this.environmentUrl}/api/Employee/LoadCsv`,
        formData,
        { headers: header }
      );
      upload$.subscribe((result) => {
        this.dialog.open(ModalMessageComponent, {
          data: result,
        });
        this.loadData();
      });
    }
  }

  loadData() {
    this.loading = true;
    this.employeeDataService
      .getEmployees(this.currentPage, this.pageSize, this.orderBy, this.orderDirection, this.searchTextValue, this.searchDateTextValue)
      .subscribe((employeePaginator) => {
        this.dataSource.data = employeePaginator.pageItems;
        this.paginator.pageIndex = employeePaginator.currentPageIndex;
        this.paginator.length = employeePaginator.totalItems;
        this.loading = false;
      });
  }

  clearSearchTextFn(): void {
    this.searchTextValue = "";
    this.loadData();
  }
  searchDateFn(): void {
    // if searchDate is valid then send request
    if(this.searchDate.valid){
      this.loadData();
    }
  }

  clearSearchDateFn(): void {
    this.searchDateTextValue = "";
    this.loadData();
  } 


  pageChanged(event) {
    console.log({ event });
    this.pageSize = event.pageSize ?? 5;
    this.currentPage = event.pageIndex ?? 1;
    this.loadData();
  }

  editEmployee(employee) {
    this.dialog.open(EmployeeEditComponent, {
      data: employee,
    });
  }
}
