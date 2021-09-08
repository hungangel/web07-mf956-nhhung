import ResourceVI from "../resource";
import * as EnumGeneral from "../enum/enumgeneral";
class CommonFn {
    /**
     * Lấy key của enum theo giá trị
     * @param {Tên enum} enumClass 
     * @param {Giá trị của enum} enumValue 
     * @returns 
     */
    getEnumText(enumClass, enumValue) {

        let enumResource = ResourceVI[enumClass]
        let enumEnum = EnumGeneral[enumClass.toString().toUpperCase()];

        for (let eName in enumResource) {
            if (enumEnum[eName] == enumValue) {
                return enumResource[eName]
            }
        }
    }

    /**
     * Kiểm tra 1 xâu cs phải xâu ngày tháng không
     * @param {xâu cần kiểm tra} dateStr 
     * @returns 
     */
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

    /**
     * Lấy key của 1 đối tượng theo giá trị truyền vào
     * @param {Đối tượng chứa dữ liệu} object 
     * @param {giá trị của trường đó} value 
     * @returns 
     */
    getKeyByValue(object, value) {
        return Object.keys(object).find(key => object[key] === value);
    }
}

export default new CommonFn()