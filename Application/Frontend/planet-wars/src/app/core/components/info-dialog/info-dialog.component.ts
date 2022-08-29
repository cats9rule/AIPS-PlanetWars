import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { InfoDialogData } from 'core/interfaces/info-dialog-data';

@Component({
  selector: 'app-info-dialog',
  templateUrl: './info-dialog.component.html',
  styleUrls: ['./info-dialog.component.scss'],
})
export class InfoDialogComponent implements OnInit {
  public title: string = '';
  public message: string = '';

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: InfoDialogData,
    private dialogRef: MatDialogRef<InfoDialogComponent>
  ) {}

  ngOnInit(): void {
    this.title = this.data.title;
    this.message = this.data.message;
  }
}
