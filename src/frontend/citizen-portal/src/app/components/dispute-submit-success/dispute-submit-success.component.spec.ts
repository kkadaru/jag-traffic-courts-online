import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { TranslateModule } from '@ngx-translate/core';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';

import { DisputeSubmitSuccessComponent } from './dispute-submit-success.component';
import { Component } from '@angular/core';

// Added the declaration of BlankComponent to be used for routing
@Component({ selector: 'app-test-blank', template: `` })
class BlankComponent {}

describe('DisputeSubmitSuccessComponent', () => {
  let component: DisputeSubmitSuccessComponent;
  let fixture: ComponentFixture<DisputeSubmitSuccessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterModule.forRoot([]),

        RouterTestingModule.withRoutes([
          { path: 'ticket/find', component: BlankComponent },
        ]),
        BrowserAnimationsModule,
        NgxMaterialModule,
        TranslateModule.forRoot(),
      ],
      declarations: [DisputeSubmitSuccessComponent, BlankComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DisputeSubmitSuccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
