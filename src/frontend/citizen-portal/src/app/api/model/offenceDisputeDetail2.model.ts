/**
 * Citizen API
 * Citizen API, for Citizen Portal frontend
 *
 * The version of the OpenAPI document: V0.1
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */
import { Dispute } from './dispute.model';
import { DisputeStatus } from './disputeStatus.model';


export interface OffenceDisputeDetail2 { 
    id: number;
    offenceNumber: number;
    requestReduction: boolean;
    reductionAppearInCourt?: boolean;
    requestMoreTime: boolean;
    reductionReason?: string;
    moreTimeReason?: string;
    status: DisputeStatus;
    offenceAgreementStatus?: string;
    disputeId: number;
    dispute?: Dispute;
}

