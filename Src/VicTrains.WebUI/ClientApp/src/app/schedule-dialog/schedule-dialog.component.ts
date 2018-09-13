import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { TrainLineListModel } from '../app.generated';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

@Component({
  selector: 'vts-schedule-dialog',
  templateUrl: './schedule-dialog.component.html',
  styleUrls: ['./schedule-dialog.component.scss']
})
export class ScheduleDialogComponent implements OnInit {
  form: FormGroup;
  trainLines: TrainLineListModel[];

  constructor(private fb: FormBuilder,
    private dialogRef: MatDialogRef<ScheduleDialogComponent>,
    @Inject(MAT_DIALOG_DATA) {
      id, trainLineId, departureStation, departureDateTime,
      arrivalStation, arrivalDateTime, trainLines }: any) {

    this.trainLines = trainLines;

    // create form fields
    this.form = fb.group({
      id: [id, Validators.required],
      trainLineId: [trainLineId, Validators.required],
      departureStation: [departureStation, Validators.required],
      departureDateTime: [departureDateTime, Validators.required],
      arrivalStation: [arrivalStation, Validators.required],
      arrivalDateTime: [arrivalDateTime, Validators.required],
    });
  }

  ngOnInit() {
  }

  save() {
    this.dialogRef.close(this.form.value);
  }

  close() {
    this.dialogRef.close();
  }
}
