import { Component, Inject, Input, OnInit } from '@angular/core';
import { MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';
import { SnackbarMessage } from '../../interfaces/snackbar-message';

@Component({
  templateUrl: './snackbar-message.component.html',
  styleUrls: ['./snackbar-message.component.scss'],
})
export class SnackbarMessageComponent implements OnInit {
  constructor(@Inject(MAT_SNACK_BAR_DATA) public data: SnackbarMessage) {}

  ngOnInit(): void {}
}
