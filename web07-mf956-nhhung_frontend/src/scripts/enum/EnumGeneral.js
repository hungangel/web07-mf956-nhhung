// export default class EnumGeneral {

export const GENDER = {
    Male: 0,
    Female: 1,
    Other: 2
}

export const VALIDATE_CODE = {
    Ok: 0,
    NotMatch: 1,
    NotNull: 2,
    InvalidFormat: 3,
    InvalidValue: 4,
    ContainAlphabetsOnly: 5,
    Duplicated: 6,
    Existed: 7,
}

export const MESSAGE_MODE = {
    Notify: 0,
    Confirm: 1,
    Full: 2,
    Alert: 3,
    Inform: 4,
}

export const TABLE_OPTION = {
    Edit: 0,
    Delete: 1,
    Duplicate: 2,
    StopUsing: 3
}

export const FORM_MODE = {
    Add: 0,
    Update: 1,
    Duplicate: 2,
}

export const FORM_ACTION = {
    SaveAndAdd: 0,
    SaveAndClose: 1,
    UpdateAndAdd: 2,
    UpdateAndClose: 3,
}

export const HTTP_STATUS = {
    Ok: 200,
    Created: 201,
    NoContent: 204,
    BadRequest: 400,
}

export default { GENDER, MESSAGE_MODE, TABLE_OPTION, FORM_MODE, FORM_ACTION, HTTP_STATUS }

// }