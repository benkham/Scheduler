import { ConfirmDialogComponent } from './../shared/confirm-dialog/confirm-dialog.component';
import { Component, OnInit } from '@angular/core';
import { ScheduleListModel, TrainLineListModel, UpdateScheduleCommand } from '../app.generated';
import { SchedulesClient, TrainLinesClient } from '../app.generated';
import { MatTableDataSource } from '@angular/material';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { ScheduleDialogComponent } from './../schedule-dialog/schedule-dialog.component';
import { MatSnackBar } from '@angular/material';

export interface DialogData {
  scheduleId: number;
  confirmed: boolean;
}

@Component({
  selector: 'vts-container',
  templateUrl: './container.component.html',
  styleUrls: ['./container.component.scss']
})

export class ContainerComponent implements OnInit {
  schedules: ScheduleListModel[];
  trainLines: TrainLineListModel[];
  displayedColumns: string[] = ['trainLine', 'departureStation', 'departureDateTime', 'arrivalStation', 'arrivalDateTime', 'actions'];
  dataSource: MatTableDataSource<ScheduleListModel>;

  constructor(private client: SchedulesClient,
    private linesClient: TrainLinesClient,
    private dialog: MatDialog,
    public snackBar: MatSnackBar) { }

  ngOnInit() {
    // get schedules from the back-end
    this.getSchedules();

    // get train lines from the back-end
    this.getTrainLines();
  }

  // filter data in the data table
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  // open dialog to add a schedule
  addTrainSchedule() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      id: 0, trainLineId: 1, departureStation: '', departureDateTime: '',
      arrivalStation: '', arrivalDateTime: '', trainLines: this.trainLines
    };

    const dialogRef = this.dialog.open(ScheduleDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      val => {
        // create schedule
        if (val.id === 0) {
          this.createSchedule(val);
        }
      }
    );
  }

  // open dialog to edit the schedule
  editSchedule({ id, trainLineId, departureStation, departureDateTime, arrivalStation,
    arrivalDateTime }: ScheduleListModel, trainLines: TrainLineListModel) {

    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      id, trainLineId, departureStation, departureDateTime, arrivalStation,
      arrivalDateTime, trainLines
    };

    const dialogRef = this.dialog.open(ScheduleDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      val => {
        // update schedule
        if (val.id > 0) {
          this.updateSchedule(val);
        }
      }
    );
  }

  getSchedules() {
    this.client.getAll().subscribe(result => {
      this.schedules = result;
      this.dataSource = new MatTableDataSource(result);
    }, error => console.error(error));
  }

  getTrainLines() {
    this.linesClient.get().subscribe(result => {
      this.trainLines = result;
    }, error => console.error(error));
  }

  createSchedule(schedule: UpdateScheduleCommand) {
    this.client.create(schedule).subscribe(result => {
      const message = 'The schedule has been created successfully!';
      this.showResponse(message, 'x');

      // refresh data source
      this.getSchedules();
    }, error => console.error(error));
  }

  updateSchedule(schedule: UpdateScheduleCommand) {
    this.client.update(schedule.id, schedule).subscribe(result => {
      const message = 'The schedule has been updated successfully!';
      this.showResponse(message, 'x');

      // refresh data source
      this.getSchedules();
    }, error => console.error(error));
  }

  confirmDeleteSchedule(schedule: ScheduleListModel) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = { scheduleId: schedule.id, confirmed: false };

    const dialogRef = this.dialog.open(ConfirmDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      val => {
        // delete schedule if confirmed
        if (val.scheduleId > 0 && val.confirmed) {
          this.deleteSchedule(val.scheduleId);
        }
      }
    );
  }

  deleteSchedule(scheduleId: number) {
    this.client.delete(scheduleId).subscribe(result => {
      const message = 'The schedule has been deleted successfully!';
      this.showResponse(message, 'x');

      // refresh data source
      this.getSchedules();
    }, error => console.error(error));
  }

  showResponse(message: string, action?: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
