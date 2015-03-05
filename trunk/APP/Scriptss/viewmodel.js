function ViewModel() {
    var services_host = "http://10.16.77.54/selfhost/";
    self = this;
    self.contacts = ko.observableArray(); //��ǰ��ϵ���б�
    self.contact = ko.observable(); //��ǰ�༭��ϵ��

    //��ȡ��ǰ��ϵ���б�
    self.load = function () {
        $.ajax({
            url: services_host + "api/contacts",
            type: "GET",
            success: function (result) {
                self.contacts(result);
            }
        });
    };

    //�����༭��ϵ�˶Ի���
    self.showDialog = function (data) {
        //ͨ��Id�ж�"���/�޸�"����
        if (!data.Id) {
            data = { ID: "", Name: "", PhoneNo: "", EmailAddress: "", Address: "" }
        }
        self.contact(data);
        $(".modal").modal('show');
    };

    //����Web API���/�޸���ϵ����Ϣ
    self.save = function () {
        $(".modal").modal('hide');
        if (self.contact().Id) {
            $.ajax({
                url: services_host + "api/contacts/" + self.contact.Id,
                type: "PUT",
                data: self.contact(),
                success: function () {
                    self.load();
                }
            });
        }
        else {
            $.ajax({
                url: services_host + "api/contacts",
                type: "POST",
                data: self.contact(),
                success: function () {
                    self.load();
                }
            });
        }
    };

    //ɾ��������ϵ��
    self.delete = function (data) {
        $.ajax({
            url: services_host + "api/contacts/" + data.Id,
            type: "DELETE",
            success: function () {
                self.load();
            }
        });
    };

    self.load();
}

$(function () {
    ko.applyBindings(new ViewModel());
});