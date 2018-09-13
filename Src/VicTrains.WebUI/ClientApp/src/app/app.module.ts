import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { SchedulesClient } from './app.generated';
import { TrainLinesClient } from './app.generated';
import { NavmenuComponent } from './navmenu/navmenu.component';
import { ContainerComponent } from './container/container.component';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDatepickerModule,
  MatDialogModule,
  MatInputModule, MatListModule, MatPaginatorModule, MatProgressSpinnerModule, MatSelectModule, MatSidenavModule,
  MatSortModule,
  MatTableModule,
  MatToolbarModule
} from '@angular/material';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { ScheduleDialogComponent } from './schedule-dialog/schedule-dialog.component';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ConfirmDialogComponent } from './shared/confirm-dialog/confirm-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    NavmenuComponent,
    ContainerComponent,
    ScheduleDialogComponent,
    ConfirmDialogComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatDatepickerModule,
    MatDialogModule,
    MatInputModule,
    MatListModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    MatSidenavModule,
    MatSortModule,
    MatTableModule,
    MatToolbarModule,
    MatMomentDateModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    MatSnackBarModule,
    RouterModule.forRoot([
      { path: '', component: ContainerComponent, pathMatch: 'full' },
    ])
  ],
  providers: [
    SchedulesClient,
    TrainLinesClient
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    ScheduleDialogComponent,
    ConfirmDialogComponent
  ]
})
export class AppModule { }
