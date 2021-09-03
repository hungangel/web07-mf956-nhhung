import ResourceVI from "./ResourceVI";
import EnumGeneral from "./enum/EnumGeneral";
class CommonFn {
    getEnumText(enumClass, enumValue) {

        let enumResource = ResourceVI[enumClass];
        let enumEnum = EnumGeneral[enumClass];

        for (let eName in enumResource) {
            if (enumEnum[eName] == enumValue) {
                return enumResource[eName]
            }
        }
    }


    isaDate(dateStr) {
        dateStr = dateStr + "";
        let dateArr = dateStr.split("T");
        if (
            (dateStr.search("-") > 0 || dateStr.search("/") > 0) &&
            (dateStr.length == 19 || dateStr.length == 10) &&
            dateArr.length <= 2
        ) {
            return true;
        }
        return false;
    }
}

export default new CommonFn()