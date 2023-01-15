import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LoadResult } from '../model/loadResult';

@Component({
  selector: 'app-modal-message',
  templateUrl: './modal-message.component.html',
  styleUrls: ['./modal-message.component.css']
})
export class ModalMessageComponent {
constructor(
  public dialogRef: MatDialogRef<ModalMessageComponent>,
    @Inject(MAT_DIALOG_DATA) public data: LoadResult,
){}
}
