# **Caffe Bar VP Project**
Windows Forms project by: Mario Mladenovik and Andrej Topukov
---

#1. Опис на апликацијата

Апликацијата која што ја развивме се однесува на работа на еден кафе бар. Самата апликација чува информации за масите на кафе барот (кои маси се слободни, на која маса
колку и кои производи се нарачани), при што за секоја маса е задолжен еден вработен (Employee). Вработениот може да биде задолжен за повеќе маси и негова задача е да ги
испорача нарачките на корисниците (Customers) и соодветно да издаде фискални сметки за испорачаните нарачки. Корисниците можат да резервираат слободна маса или да направат нарачка која ќе им биде доставена на внесената адреса. Резервацијата на слободна маса е возможна само доколку е нарачана одредена количина на производи (со секоја нарачка која надминува 400 денари). За секоја нарачка се креира тајмер којшто мери за колку време нарачката ќе биде испорачана до корисникот. Доколку времето на испорака е поголемо од 25 минути, корисникот добива 20% попуст на цената на нарачката. Производите за нарачка или резервација на маса, корисникот ги селектира од приложено мени на кафе барот, при што апликацијата води евиденција за тоа кој производ го има кафе барот на залиха и доколку одреден производ го нема на залиха, апликацијата го известува корисникот. Сите продукти во менито на кафе барот се групирани според категоријата на продукти во која припаѓаат.
При внес на нов продукт во кафе барот, на почетната форма од апликацијата се врши промоција на тој продукт. Корисникот за да може да прави нарачки или резервации мора
да биде логиран со соодветно корисничко име и лозинка или доколку е нов корисник, мора да креира нова корисничка сметка. Апликацијата сите податоци ги чува во бази на
податоци.

##2. База на податоци

Во овој дел од документацијата ќе биде опишана базата на податоци. Самата база претставува клучен дел од нашиот проект, бидејќи сите податоци се содржат во неа.
Податоците во базата се чуваат во соодветни релации кои се меѓусебно поврзани со врски. Релациите во базата со нивните атрибути се следните:

- dbo.Categories (catId: int PK, catName: string) -> категории на продуктите

- dbo.Customers (custId: int PK, custName: string, custSurname: string, custTelephone: string, custUsername: string, custPassword: string, address: string, age: int, email: string, loggedIn: string) -> релација за корисници

- dbo.Employees (empId: int PK, empName: string, empSurname: string, empTelephone: string, empUsername: string, empPassword: string, pay: int, payPerHour: int, 
workHours: int, loggedIn: int) -> релација за вработени

- dbo.Orders (orderId: int PK, empId: int FK (dbo.Employee) NOT NULL, custId: int FK (dbo.Customers) NOT NULL, tableId: int FK (dbo.Tables) NULL, status: int,
timeToDeliver: int, orderAddress: string, orderPrice: int) -> релација за нарачки

- dbo.Products (proId: int PK, catId: int FK (dbo.Categories) NOT NULL, proPrice: int, proName: string, timeOfServing: int, ageRestrictions: int, proDescription: string, proQuantity: int) -> релација за продукти

- dbo.ProductsInOrder (id: int PK, productId: int FK (dbo.Products) NOT NULL, orderId: int FK (dbo.Orders) NOT NULL) -> релација која чува податоци за тоа кој продукт во која нарачка припаѓа

- dbo.ProductsInReservation (id: int PK, productId: int FK (dbo.Products) NOT NULL, resId: int FK (dbo.Reservations) NOT NULL) -> релација која чува податоци за тоа кој продукт во која резевација припаѓа

- dbo.Reservations (id: int PK, custId: int FK (dbo.Customers) NOT NULL, tableId: int FK (dbo.Tables) NOT NULL, dateRes: Date, numPeople: int, MinPriceRes: const int (=400), priceRes: int) -> релација за резервациите

-dbo.Tabels (tableId: int PK, empId: int FK (dbo.Employees), numberOfSeats: int, tableAvailable: int) -> релација за масите во кафе барот

Врските помеѓу релациите се следните:
- dbo.Categories 1:M dbo.Products
- dbo.Orders M:N dbo.Products
- dbo.Tables 0,1:N dbo.Orders
- dbo.Employees 1:N dbo.Tables
- dbo.Employees 1:N dbo.Orders
- dbo.Customers 1:N dbo.Orders
- dbo.Customers 1:N dbo.Reservations

##############**ОВДЕ ТРЕБА ДА ПИШЕМЕ КАКО ЌЕ ИМ ЈА ПРАЌАМЕ БАЗАТА**####################

##3. Упатство на користење

###3.1 Главна форма (Form1)

![Слика 1](https://github.com/Mario172076/Caffe-Bar-VP-/blob/main/CaffeBar/CaffeBar/bin/img/MainForm.png?raw=true "Слика 1: Главната форма")

Почетната форма (Form1) на апликацијата е составена од три делови: дел за логирање на веќе постоечки корисник, дел за промоција на нов производ и дел за креирање на
нова корисничка сметка (регистрација на нов корисник).

###3.1.1 Дел за логирање (Login)

При логирање на веќе постоечки корисник, апликацијата најпрво проверува дали таков корисник постои во табелата за корисници (dbo.Customers) во базата на податоци.
Доколку постои корисникот, се проверува дали се внесени точното корисничко име и точната лозинка. Апликацијата преку ErrorProvider го известува корисникот доколку 
настанала грешка при логирање (корисникот не постои во базата на податоци или внесени се погрешни креденцијали) со пораката „Wrong credentials“. 
Соодветно се проверува и доколку вработен се логира, со тоа што податоците се проверуваат во табелата за вработени (dbo.Employees) во базата на податоци.
Доколку податоците се валидни и корисникот постои во базата, апликацијата го пренасочува корисникот на CustomerForm формата. Аналогно, вработениот се пренасочува на
AdminForm формата.

###3.1.2 Дел за промоција на производ (Promotion)

Промоцијата на новиот производ се врши така што апликацијата чита од базата на податоци кој е последниот внесен производ и соодветно на тоа апликацијата испишува
„NEW PRODUCT:“ и името и цената на продуктот. Во зависност од вредноста на catId колоната на продуктот од табелата за продукти (dbo.Products) во базата на податоци,
апликацијата прикажува соодветна слика. Доколку не постои слика за соодветната категорија на продукти, апликацијата ја прикажува предодредената слика „caffe-bar.jpg“,
која всушност преставува генеричка слика за кафе барот. Сликите за категориите на продуктите се чуваат во img директориумот во bin директориумот од проектот.

###3.1.3 Дел за регистрација на нов корисник (Register)

Делот за регистрација на нов корисник е сличен со делот за логирање, при што тука имаме повеќе TextBox и Label контроли. Само корисник може да се регистрира, 
вработените се директно внесени во базата на податоци. Како и во делот за логирање, со користење на ErrorProvider контроли и валидација на формите, доколку настане
проблем при внес на податоци, апликацијата го известува корисникот со соодветна порака за грешка. Кога корисникот ќе внесе валидни податоци во секоја TextBox контрола,
со клик на копчето Register се креира запис за корисникот во табелата dbo.Customers во базата на податоци.

Користени контроли во оваа форма:
- GroupBox контрола - секој од гореопишаните делови се наоѓа во посебна GroupBox контрола
- TextBox контрола - во деловите за логирање и регистрација на нов корисник има повеќе TextBox контроли кои соодветно се валидираат за да се осигура апликацијата дека
корисникот внел точни податоци
- PictureBox контрола - во делот за промоција на новиот производ, сликата се прикажува со помош на оваа контрола
- Button контрола - двете копчиња Login и Register во делот за логирање и делот за регистрација на нов корисник, соодветно
- ErrorProvider контрола - го известува корисникот доколку внесе невалидни податоци во некоја TextBox контрола
- Timer контрола - тајмерот го брои времето на пристигнување на нарачката (дополнително ќе биде објаснето кај AdminForm формата)
- При внес на невалидни податоци, апликацијата го известува корисникот со помош на MessageBox

###3.2 Форма за корисниците (CustomerForm)

![Слика 2](https://github.com/Mario172076/Caffe-Bar-VP-/blob/main/CaffeBar/CaffeBar/bin/img/CustomerForm.png?raw=true "Слика 2: Форма за корисниците")

Откако корисникот успешно ќе се логира, тој се пренасочува на оваа форма. Доколку корисникот креира нова корисничка сметка и притисне на копчето Register во главната форма, тој повторно мора да се логира во Login делот, бидејќи Register само креира нов запис во релацијата dbo.Customers. Формата за корисниците се состои од шест GroupBox контроли, дел за одјавување на корисникот и копче за преглед на фискланите сметки што ги добил корисникот.

####3.2.1 Дел за креирање на резервација (MAKE RESERVATION)

Во првата GroupBox контрола (MAKE RESERVATION) корисникот може да направи резервација на маса во кафе барот. За да се направи резервација, корисникот мора да внесе датум и/или време за резервацијата, број на луѓе, да одбере слободна маса од листата со маси и да додаде продукти. Како што беше наведено претходно, за да може да се креира резервација, корисникот мора да нарача продукти чија цена надминува 400 денари. Резервацијата нема да биде успешна ако вкупната цена на нарачаните продукти е помала од 400 денари. Апликацијата го известува корисникот со соодветна MessageBox порака. Исто така, доколку некои од внесените податоци се празни или невалидни, апликацијата повторно прикажува MessageBox со соодветна порака. Во овој дел се наоѓаат две копчиња, Make Reservation и Remove Products. Make Reservation копчето ја креира резервацијата (запишува во db.Reservations релацијата) и ги пребришува вредностите на сите листи и текст полиња кои се наоѓаат во оваа GroupBox контрола. Копчето Remove Products ја чисти ComboBox контролата во која се листаат продуктите нарачани во тековната резервација.

####3.2.2 Дел за креирање на нарачка (MAKE ORDER)

Втората GroupBox контрола се однесува на правење нарачки од страна на корисникот. Корисникот може да прави нарачки за маса во кафе барот или да прави нарачки за достава до адресата која ја внесува во соодветното текст поле. За да биде успешно креирањето на нарачката, корисникот мора да избере маса или да внесе адреса. Апликацијата преку MessageBox пораки соодветно го известува корисникот доколку внел невалидни податоци. Исто како и во претходниот дел, и во овој дел имаме две копчиња, Make Order и Remove Products. Make Order ја креира нарачката и ја внесува во базата на податоци. Дополнително, во ORDER STATUS делот се додава нарачката, при што се печати нејзиното ID, се додава ProgressBar контрола која ќе му кажува на корисникот до каде е доставувањето на нарачката и дополнително по ProgressBar контролата се испишува статус „pending“, што значи дека се чека некој вработен да ја испрати нарачката. Копчето Remove Products е идентично како и во делот за резервации, ја пребришува ComboBox контролата која ги содржи продуктите за тековната нарачка.

####3.2.3 Менито на кафе барот (MENU)

Третата GroupBox контрола содржи неколку ListBox контроли со соодветни Label контроли и две копчиња, Add products to order и Add products to reservation. Во ListBox контролите се листаат соодветните продукти од категоријата која е впишана во Label контролата која се наоѓа над листата. Корисникот може да селектира повеќе продукти во дадено време, од повеќе листи. Кога ќе ги селектира посакуваните продукти, корисникот со клик на копчето Add products to order ги додава продуктите во тековната нарачка. Аналогно на тоа, со клик на Add products to reservation копчето корисникот ги додава селектираните продукти во тековната резервација. Доколку корисникот нема селектирано продукт, а кликне на некое од овие копчиња, нема ништо да се случи. Дополнително, апликацијата води евиденција за тоа дали го има продуктот на залиха во кафе барот. Се проверува вредноста на proQuantity атрибутот за секој продукт во dbo.Products релацијата. Доколку вредноста на овој атрибут за некој продукт е 0, тогаш тој продукт нема да се прикаже во менито.

####3.2.4 Статус на нарачките (ORDER STATUS)

Во овој дел од CustomerForm формата, апликацијата ги прикажува ID вредноста и статусот на нарачките направени од корисникот (нарачки за испорака), како и до каде е нивната испорака, доколку се испорачани од некој вработен. Во оваа GroupBox контрола се наоѓаат 8 лабели и 4 ProgressBar контроли. Вредностите на ProgressBar контролите се сетираат со секој tick на Timer контролата, при што доколку ја достигнат максималната вредност, лабелите и ProgressBar контролите за соодветната нарачка не се прикажуваат повеќе и статусот на нарачката во базата на податоци се сетира на вредност 3 (испорачана). Вредностите за ProgressBar контролите, во tick() функцијата тајмерот се земаат од Properties.Settings.Default. Во Settings делот тие се запишуваат од  страна на Timer контролата во главната форма. Бидејќи главната форма се прикажува непрекинато во позадина, таа на секоја секунда ажурира вредности на 4 променливите кои се чуваат во Settings делот на апликацијата. Оваа форма ги користи тие 4 променливи за да ги ажурира вредностите на ProgressBar контролите.

####3.2.5 Дел за одјавување

Делот за одјавување во оваа форма се состои од три контроли: една Label, една TextBox и една Button контрола. Label и TextBox контролите кажуваат кој е моментално логираниот корисник, при што неговото име е запишано во TextBox контролата. Со клик на копчето Logout, корисникот може да се одјави од апликацијата, при што самата апликација го пренасочува во главната форма. При одјавувањето, во релацијата dbo.Customers од базата на податоци, вредноста на атрибутот loggedIn кај соодветниот корисник се сетира на 0. При логирање, овој атрибут се сетира на 1. Корисникот може и директно да се одјави од апликацијата со исклучување на оваа форма, при што и во овој случај атрибутот loggedIn во базата на податоци се сетира на 0.

####3.2.6 Копчето View Receipt(s)

Со клик на копчето View Receipt(s), на корисникот му се прикажува нова форма, ReceiptForm. Во оваа форма се прикажуваат фискалните сметки за испорачаните нарачки на корисниците. Фискална сметка за нарачка се издава од вработен и таа може да биде издаден и пред нарачката да пристигне кај корисникот (подетално за ова во AdminForm формата).

Користени контроли во оваа форма:
- GroupBox контрола - за групирање на останатите контроли
- TextBox контрола - во деловите за креирање нарачка и резервација има TextBox контроли за корисникот да може да внесе податоци; исто така одредени податоци се прикажани во TextBox контроли
- ComboBox контрола - слободните маси (масите кои корисникот може да ги селектира при правење резервација или нарачка) и нарачаните продукти се чуваат во соодветна ComboBox контрола
- Button контрола - вкупно 8 копчиња во оваа форма; 1 за креирање на резервација, 1 за креирање на нарачка, 2 за пребришување на листите со нарачани продукти, 1 за одјавување на корисникот, 1 за прикажување на фискалните сметки на корисникот, 1 за додавање продукти во резервација и 1 за додавање на продукти во нарачка
- ListBox контроли - за прикажување на менито на кафе барот
- Label контроли - ги објаснуваат другите контроли; во делот ORDER STATUS ги прикажуваат ID вредностите на нарачките и нивниот статус
- ErrorProvider контрола - за валидација на внесените податоци од корисникот
- ProgressBar контроли - прикажува до каде е испораката на нарачките
- Timer контрола - ги сетира вредностите на ProgressBar контролата (или контролите доколку повеќе нарачки на дадениот корисник се испорачани од вработените) и доколку некоја ProgressBar контрола ја достигне максималната вредност, соодветната нарачка не се прикажува повеќе

###3.3 Форма за прикажување на фискалните сметки на корисникот (ReceiptForm)

![Слика 3](https://github.com/Mario172076/Caffe-Bar-VP-/blob/main/CaffeBar/CaffeBar/bin/img/ReceiptForm.png?raw=true "Слика 3: Форма за прикажување на фискалните сметки на корисникот")

Оваа форма е многу едноставна. Содржи само една лабела и DataGridView контрола која ги прикажува фискланите сметки. ReceiptForm формата ќе биде опишана преку нејзината .cs датотека, чиј код е прикажан подолу.

```csharp
public partial class ReceiptForm : Form
    {
        public ReceiptForm()
        {
            InitializeComponent();
            loadInformations();

        }

        public void loadInformations()
        {
            Random rand = new Random();
            using (var context = new ModelContext())
            {
                Customer customer = context.Customer.Where(c => c.LoggedIn == 1).FirstOrDefault();
                List<Order> orders = context.Orders.Where(o => o.CustId == customer.CustId && o.Status == 3).ToList();
                List<Employee> employees = context.Employee.ToList();
                Employee employee = new Employee();
                foreach (Order o in orders)
                {

                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                    row.Cells[0].Value = rand.Next();
                    row.Cells[1].Value = o.OrderId;
                    foreach (Employee emp in employees)
                    {
                        if (o.EmpId == emp.EmpId)
                        {
                            employee = emp;
                            break;
                        }
                          
                        
                    }
                    row.Cells[2].Value = employee.EmpName;
                    List < ProductsInOrder > productsInOrder = context.ProductsInOrder.Where(pio => pio.OrderId == o.OrderId).ToList();
                    StringBuilder sb = new StringBuilder();
                    foreach (ProductsInOrder product in productsInOrder)
                    {                       
                        Product productt = new Product();
                        productt = context.Products.Where(p => p.ProId == product.ProductId).FirstOrDefault();
                        sb.Append("'");
                        sb.Append(productt.ProName);
                        sb.Append("'");
                        sb.Append(" ");
                        
                    }
                    row.Cells[3].Value = sb.ToString();
                    row.Cells[4].Value = o.OrderPrice;
                    dataGridView1.Rows.Add(row);

                }
            }
        }

    }
```
На почеток, се иницијализира Random објект, кој подоцна ќе се користи за да генерира ID вредност на сметката. Потоа, од базата на податоци се зема кој е моментално логираниот корисник и дополнително се земаат сите нарачки кои се испорачани за тој корисник. Иницијално, DataGridView контролата има една празна редица, која се клонира за секоја нарачка. Секоја редица се состои од пет колони. Понатаму, се сетираат вредностите на колоните во редицата. Во првата колона, се генерира случаен број со помош на Random објектот. Во втората колона се запишува ID вредноста на нарачката, додека пак во третата колона се запишува името на вработениот што ја направил нарачката. Следно, со помош на релацијата dbo.ProductsInOrder се земаат сите продукти од тековната нарачка, при што нивните имиња се запишуваат во StringBuilder променлива. Имињата се одвоени со празно место и се запишуваат со единечни наводници. Во четвртата колона се запишува string вредноста на StringBuilder променливата. Во петтата колона се запишува цената на нарачката. На крај, редицата се додава во DataGridView контролата. Ова се повторува за сите испорачани нарачки на корисникот кој е моментално логиран.
Сето ова се прави во функцијата loadInformations() која се повикува веднаш по иницијализацијата на формата.

Користени контроли:
- Label контрола - за приказ на насловот на формата
- DataGridView контрола - за приказ на фискалните сметки за моментално најавениот корисник

###3.4 Форма за вработените (AdminForm)

![Слика 4](https://github.com/Mario172076/Caffe-Bar-VP-/blob/main/CaffeBar/CaffeBar/bin/img/AdminForm.png?raw=true "Слика 4: Форма за вработените")

Формата за вработените (AdminForm) е клучна форма во апликацијата. Преку неа, вработените може да додаваат нови категории на продукти, нови продукти, нови маси, да вршат испорака на нарачки, да издаваат фискални сметки и сл. Апликацијата ги пренасочува вработените во оваа форма, доколку тие успешно се логираат.
AdminForm формата се состои од седум делови, од кои шест се наоѓаат во GroupBox контрола. Седмиот дел кој е за одјавување на вработениот е идентичен како и во формата за корисниците (CustomerForm).

####3.4.1 Преглед на масите (TABLES)

Првиот дел од AdminForm формата се однесува на додавање на нова маса и преглед на веќе постоечка маса. Во GroupBox контрола е сместена една ListBox контрола која ги прикажува сите маси и две копчиња, Add table и View Table, за додавање и преглед на маса, соодветно. Доколку вработениот притисне на копчето View table, но не селектира маса, тогаш апликацијата соодветно преку MessageBox ќе го извести вработениот дека мора да селектира маса, а доколку е селектирана маса, тогаш вработениот се пренасочува на нова форма за преглед на масата. При клик на копчето Add table се отвара нова форма за да може вработениот да додаде нова маса во апликацијата. Овие две форми се опишани подолу.

#####3.4.A Форма за додавање на нова маса во апликацијата (AddTableForm)

![Слика 5](https://github.com/Mario172076/Caffe-Bar-VP-/blob/main/CaffeBar/CaffeBar/bin/img/AddTableForm.png?raw=true "Слика 5: Форма за додавање на нова маса во апликацијата")

Со помош на оваа форма вработениот внесува нова маса во апликацијата. Формата се состои од една TextBox контрола, две ComboBox контроли и две копчиња, Add table и Cancel. Дополнително, формата содржи и лабели за опис на некои од контролите. Во TextBox контролата, вработениот потребно е да внесе колку места за седење може да има на масата, потоа во Available ComboBox контролата специфицира дали масата е слободна или не и на крај во Employee ComboBox контролата специфицира кој вработен ќе биде задолжен за таа маса. ErrorProvider контрола валидира дали е внесен валиден податок во TextBox контролата. Со клик на копчето Add table, доколку внесените податоци се валидни, вработениот се известува дека успешно е внесена масата во апликацијата и самата апликација креира нов запис во dbo.Tables релацијата од базата на податоци.

Користени контроли:
- Label контроли
- ComboBox контроли
- Button контроли

#####3.4.B Форма за преглед на веќепостоечка маса (TableInfoForm)

![Слика 6](https://github.com/Mario172076/Caffe-Bar-VP-/blob/main/CaffeBar/CaffeBar/bin/img/TableInfo.png?raw=true "Слика 6: Форма за преглед на веќепостоечка маса")

TableInfoForm формата ги прикажува сите податоци за масата којашто вработениот претходно ја селектираше. Сите податоци се прикажани во TextBox контроли освен нарачаните продукти, кои се прикажуваат во ComboBox контрола. Доколку масата е слободна, тогаш нема продукти за прикажување, па така ComboBox контролата е празна. Тоа зависи од вредноста на Available текст полето.

Користени контроли:
- Label контроли
- TextBox контроли
- ComboBox контрола

####3.4.2 Дел за прегледување, испорака и издавање на фискални сметки за нарачките (ORDERS)

Вториот дел од AdminForm формата се однесува на нарачките во кафе барот. Преку контролите кои исто како и тие во претходниот дел се групирани во GroupBox контрола, вработениот може да ја прегледува нарачката, да ја испорача и да издаде фискална сметка за веќе испорачана нарачка. Во ListBox контрола се листаат сите нарачки кои се направени од сите корисници. Вработениот може да прегледа било која нарачка со селектирање на нарачката и клик на копчето View order. MessageBox порака го известува вработениот дека е потребно да селектира нарачка, доколку не е селектирана. Кога вработениот ќе кликне на копчето Deliver order, нарачката се испорачува до корисникот. Статусот на нарачката се сетира на вредност 2 (во испорака) и оваа промена се евидентира во базата на податоци. Од друга страна, со клик на копчето Give receipt се издава фискална сметка за нарачката со тоа што нејзиниот статус се сетира на 3 (без оглед на тоа дали е пристигната или не), се евидентираат промените во базата на податоци и статусот на нарачката се сетира на вредност 3 (испорачана). Функционалностите на двете копчиња се опишани подолу.

#####3.4.C Форма за преглед на нарачката (OrderDetailsForm)

![Слика 7](https://github.com/Mario172076/Caffe-Bar-VP-/blob/main/CaffeBar/CaffeBar/bin/img/OrderDetailsForm.png?raw=true "Слика 7: Форма за преглед на нарачката")

OrderDetailsForm формата ги прикажува сите податоци за селектираната нарачка. Составена е од TextBox контроли со својство Read Only, со цел да не може да се менуваат податоците на нарачката. Исто така, содржи и една ComboBox компонента, во која се листаат податоците кои се нарачани во нарачката.

Користени контроли:
- Label контрола
- TextBox контрола
- ComboBox контрола

#####3.4.D Deliver button копчето

Функцијата која се повикува при клик на копчето Deliver button е следната:
```csharp
 private void btnDeliverOrderAF_Click(object sender, EventArgs e)
        {
            if (lbOrdersAF.SelectedIndex != -1)
            {
                Order o = (Order)lbOrdersAF.SelectedItem;
                Random random = new Random();
                int timeToDeliver = random.Next(minValue: 15, maxValue: 31);
                timeOfDelivery = timeToDeliver;
                o.TimeToDeliver = timeToDeliver;
                // dodaj order vo FinishedOrders
                using (var context = new ModelContext())
                {
                    Order order = new Order();
                    try
                    {
                        order = context.Orders.Where(ord => ord.OrderId == o.OrderId && o.OrderAddress != null).FirstOrDefault();
                        if (order == null)
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("Please select an order that can be delivered (has an address)");
                        return;
                    }

                    order.Status = 2; // order is delivering
                    context.Entry(order).State = EntityState.Modified;
                    context.SaveChanges();

                    int orderId = o.OrderId;
                    Customer customer = context.Customer.Where(c => c.CustId == o.CustId).FirstOrDefault();
                    List<Order> orders = context.Orders.Where(ord => ord.CustId == customer.CustId && ord.Status == 2).ToList();
                    int orderCount = 0;
                    for (int i = 0; i < orders.Count; i++)
                    {

                        if (orderId == orders[i].OrderId)
                        {
                            orderCount = i + 1;
                            break;
                        }
                    }
                    List<Order> orders2 = context.Orders.Where(ord => ord.CustId == customer.CustId && ord.Status == 2).ToList();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("timeElapsed");
                    sb.Append(orderCount);
                    if ("timeElapsed1" == sb.ToString())
                    {
                        Properties.Settings.Default.timeElapsed1 = 0;

                    }
                    else if ("timeElapsed2" == sb.ToString())
                    {

                        Properties.Settings.Default.timeElapsed2 = 0;

                    }
                    else if ("timeElapsed3" == sb.ToString())
                    {
                        Properties.Settings.Default.timeElapsed3 = 0;

                    }
                    else if ("timeElapsed4" == sb.ToString())
                    {
                        Properties.Settings.Default.timeElapsed4 = 0;

                    }
                    counterDeliver++;
                    sb.Clear();

                }


                order = o;
                lbOrdersAF.SelectedIndex = -1;
                MessageBox.Show("Order deivered successfully");
                deliveredOrders.Add(o);

            }
            else
            {
                MessageBox.Show("No order is selected");
            }
        }
```
На почеток, се зема селектираната нарачка од листата со нарачки и за неа се генрира случаен број во опсегот [15, 31]. Овој број всушност претставува колку време во минути е потребно за нарачката да се испорача до корисникот. Следно се проверува дали може нарачката да се испорача до корисникот, односно дали корисникот внел адреса.
Доколку корисникот не внел адреса, апликацијата го известува вработениот со соодветен MessageBox. Потоа, се сетира статусот на нарачката на вредност 2 (во испорака) и промените се зачувуваат во базата на податоци. Понатаму, од базата на податоци се зема корисникот кој ја направил селектираната нарачка и за него се земаат сите нарачки што ги има направено. Всушност, се проверува колку нарачки направил корисникот и во однос на тоа која по ред е селектираната нарачка, соодветно се сетира на 0 некоја од променливите timeElapsed. Има 4 timeElapsed променливи, timeElapsed1, timeElapsed2, timeElapsed3 и timeElapsed4. Секоја од овие променливи го мери времето до каде е испораката на соодветната нарачка. Овие 4 променливи се чуваат во Properties.Settings.Default делот од апликацијата, за да можат да се препраќаат помеѓу формите. Нивните вредности се ажурираат со tick() функциите на Timer контролите во главната форма (MainForm) и формата за корисници (CustomerForm). ProgressBar контролите се ажурираат соодветно со tick() функцијата во CustomerForm формата.
Со други зборови, со сетирање на соодветната timeElapsed променлива, Timer контролите од главната форма и формата за корисниците ја ажурираат нејзината вредност, при што оваа вредност на секој повик на tick() функцијата во CustomerForm формата се задава како вредност на соодветниот ProgressBar.

#####3.4.E Give receipt копчето

Со клик на копчето Give receipt се повикува следната функција:
```csharp
private void btnReceiptOrderAF_Click(object sender, EventArgs e)
        {
            var context = new ModelContext();
            if (lbOrdersAF.SelectedIndex == -1)
            {
                MessageBox.Show("No order is selected");
                return;
            }
            else
            {
                Order o = (Order)lbOrdersAF.SelectedItem;

                if (o.OrderAddress != "" && o.OrderAddress != null && o.Status == 1)
                {
                    MessageBox.Show("Please deliver the order first");
                    return;
                }

                List<Order> orders = context.Orders.Where(or => (or.TableId != null && or.Status == 1) || (or.OrderAddress != null && or.Status == 2)).ToList();
                bool flag = false;
                foreach (Order order in orders)
                {
                    if (order.OrderId == o.OrderId)
                    {
                        flag = true;
                        o = order;
                    }
                }
                if (flag == true)
                {
                    // pusti smetka
                    StringBuilder strb = new StringBuilder();
                    String empName = context.Employee.Where(emp => emp.EmpId == o.EmpId).Select(emp => emp.EmpName).FirstOrDefault();
                    strb.Append("ReceiptID: ").Append(rand.Next()).Append(" OrderID: ").Append(o.OrderId).Append(" Employee Name: ").Append(empName).Append(" Products: ");
                    List<ProductsInOrder> productsOrdered = context.ProductsInOrder.Where(x => x.OrderId == o.OrderId).ToList();
                    List<Product> products = new List<Product>();
                    foreach (ProductsInOrder pio in productsOrdered)
                    {
                        Product product = context.Products.Where(p => p.ProId == pio.ProductId).FirstOrDefault();
                        products.Add(product);
                    }

                    foreach (Product product in products)
                    {
                        strb.Append(product.ProName).Append(" ");
                    }
                    strb.Append("Price: " + o.OrderPrice);
                    if (receipts.ContainsKey(o.CustId))
                    {
                        List<string> list = receipts[o.CustId];
                        list.Add(strb.ToString());
                        receipts[o.CustId] = list;
                    }
                    else
                    {
                        List<string> list = new List<string>();
                        list.Add(strb.ToString());
                        receipts[o.CustId] = list;
                    }
                    o.Status = 3;
                    Table table = new Table();
                    if (context.Tables.Where(t => t.TableId == o.TableId).FirstOrDefault() != null)
                    {
                        table = context.Tables.Where(t => t.TableId == o.TableId).FirstOrDefault();
                        table.TableAvalaible = true;
                        context.Entry(table).State = EntityState.Modified;
                    }
                    Customer customer = context.Customer.Where(c => c.CustId == o.CustId).FirstOrDefault();
                    List<Order> orders1 = context.Orders.Where(ord => ord.CustId == customer.CustId && ord.Status == 2).ToList();
                    List<int> orderIds1 = new List<int>();
                    foreach(Order order in orders1)
                    {
                        orderIds1.Add(order.OrderId);
                    }

                    context.Entry(o).State = EntityState.Modified;
                    context.SaveChanges();

                    MessageBox.Show("Receipt delivered successfully");

                    Customer customerr = context.Customer.Where(c => c.CustId == o.CustId).FirstOrDefault();
                    List<Order> orders2 = context.Orders.Where(ord => ord.CustId == customerr.CustId && ord.Status == 2).ToList();
                    List<int> orderIds2 = new List<int>();
                    int position = 0;
                    foreach (Order order in orders2)
                    {
                        orderIds2.Add(order.OrderId);
                    }
                    for(int i=0; i<orderIds1.Count; i++)
                    {
                        if (!orderIds2.Contains(orderIds1[i]))
                        {
                            position = i + 1;
                        }
                    }
                    timeElapsed1 = Properties.Settings.Default.timeElapsed1;
                    timeElapsed2 = Properties.Settings.Default.timeElapsed2;
                    timeElapsed3 = Properties.Settings.Default.timeElapsed3;
                    timeElapsed4 = Properties.Settings.Default.timeElapsed4;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("timeElapsed");
                    sb.Append(position);
                    if(sb.ToString() == "timeElapsed1")
                    {
                        timeElapsed1 = timeElapsed2;
                        timeElapsed2 = timeElapsed3;
                        timeElapsed3 = timeElapsed4;
                        Properties.Settings.Default.timeElapsed1 = timeElapsed1;
                        Properties.Settings.Default.timeElapsed2 = timeElapsed2;
                        Properties.Settings.Default.timeElapsed3 = timeElapsed3;

                    } 
                    else if (sb.ToString() == "timeElapsed2")
                    {
                        timeElapsed2 = timeElapsed3;
                        timeElapsed3 = timeElapsed4;
                        Properties.Settings.Default.timeElapsed2 = timeElapsed2;
                        Properties.Settings.Default.timeElapsed3 = timeElapsed3;

                    } 
                    else if (sb.ToString() == "timeElapsed3")
                    {
                        timeElapsed3 = timeElapsed4;
                        Properties.Settings.Default.timeElapsed3 = timeElapsed3;
                    } 

                }
                else
                {
                    MessageBox.Show("Order has not been delivered! Deliver it first");
                    return;
                }

                updateLabels();
                loadInformations();
            }
        }
```
Функцијата најпрво проверува дали е селектирана нарачка од листата со нарачки. Доколку не е селектирана нарачка, апликацијата го известува вработениот со MessageBox. Селектираната нарачка се зема и се проверува дали е нарачка за испорака и доколку е, дали е испорачана. Доколку барем еден од овие два услови не е исполнет, вработениот е известен со MessageBox дека треба прво да ја испорача нарачката (со клик на Deliver order копчето).
Нарачките за кои не е внесена адреса, туку е внесена маса во кафе барот не треба да се испорачуваат. За нив може директно да се издаде фискална сметка. Останатите нарачки мора прво да се испорачаат па потоа за нив да се издаде фискална сметка.
Понатаму, доколку е селектирана валидна нарачка, апликацијата ја генерира фискалната сметка со помош на StringBuilder објект. Фискалната сметка се состои од ID вредност на фискалната сметка (случајно генериран цел број), име на вработениот што ја испорачал нарачката (се зема од базата на податоци), продуктите од кои се состои нарачката (исто така прочитани од базата) и цената на нарачката.
Следно, доколку нарачката е за маса во кафе барот, односно не е за испорака, се ослободува масата која била зафатена со оваа нарачка. Тоа се прави со запишување во базата на податоци во релацијата dbo.Tables, атрибутот TableAvailable станува True. Соодветно, со повик на SaveChanges() функцијата, се зачувуваат промените во базата на податоци.
Останатиот дел од функцијата врши замена на вредностите на timeElapsed променливите и со тоа замена на вредностите на ProgressBar-овите во CustomerForm формата. Ова ќе го опишеме со пример. Нека даден корисник има 4 нарачки кои се во испорака. Притоа, во CustomerForm формата во ORDER STATUS делот е прикажано следното:

1 [start]----      [finish] delivering

2 [start]---       [finish] delivering

3 [start]--        [finish] delivering

4 [start]-         [finish] delivering

Доколку вработениот издаде сметка за третата нарачка, вредноста од четвртиот ProgressBar се преместува во третиот ProgressBar, при што последниот ProgressBar не се прикажува повеќе. Односно, ситуацијата по издавање на фискална сметка за третата нарачка е следната:

1 [start]----      [finish] delivering

2 [start]---       [finish] delivering

3 [start]-         [finish] delivering

Ова се постигнува со пронаоѓање на позицијата на нарачката во листата со нарачки (конкретно ID вредности на нарачките) и соодветно менување на вредностите на timeElapsed променливите за останатите нарачки. Така, во горенаведениот пример, timeElapsed3 ја зема вредноста на timeElapsed4. Потоа овие променливи соодветно се ажурираат во Properties.Settings делот. if-else деловите ги опфаќаат сите можни сценарија за ваквото менување на вредностите. 

####3.4.3 Преглед на резервациите (RESERVATIONS)

Во третиот дел од AdminForm формата, вработениот може да ги прегледува сите направени резервации. Овој дел се состои од една ListBox контрола и една Button контрола групирани во една GroupBox контрола. Вработениот за да прегледа некоја резервација треба да ја селектира од ListBox контролата и потоа да притисне на копчето View Reservation. Доколку притисне на копчето, а не е селектирана резервација, тогаш апликацијата соодветно го известува вработениот преку MessageBox. Доколку е селектирана резервација, со клик на копчето View Reservation вработениот се пренасочува во формата ReservationDetailsForm.

#####3.4.F Форма за детален преглед на постоечка резервација (ReservationDetailsFrom)

![Слика 8](https://github.com/Mario172076/Caffe-Bar-VP-/blob/main/CaffeBar/CaffeBar/bin/img/ReservationDetailsForm.png?raw=true "Слика 8: Форма за детален преглед на постоечка резервација")

ReservationDetailsForm формата е многу слична на TableInfoForm формата. Се состои од лабели, текст полиња (со својство Read Only) и една ComboBox контрола. Лабелите ги опишуваат другите контроли и го прикажуваат описот на формата, текст полињата ги прикажуваат податоците за резервацијата, додека пак ComboBox контролата ги листа сите нарачани продукти на резервацијата.

Користени контроли:
- Label контроли
- TextBox контроли
- ComboBox контрола

####3.4.4 Додавање на нов продукт (ADD PRODUCT)

Делот за додавање на нов продукт во апликацијата се состои од една GropuBox контрола во која се групирани повеќе Label и TextBox контроли, две ComboBox контроли и едно копче Add product. Вработениот потребно е да внесе валидни податоци за продуктот и од двете ComboBox контроли да избере соодветно дали продуктот е за лица над 18 години (ComboBox контролата Age Restriction) и соодветна категорија за продуктот (ComboBox контролата Category). Вработениот е известен со соодветен MessageBox доколку не ги внел потребните податоци, а ErrorProvider контролата го известува доколку внесените податоци се невалидни. Откако вработениот ќе внесе валидни податоци, со клик на копчето Add product се додава продуктот во апликацијата. Всушност, се запишува пордуктот во dbo.Product релацијата од базата на податоци и вработениот се известува дека продуктот е успешно додаден.

####3.4.5 Додавање на нова категорија (ADD CATEGORY)

Додавањето на нова категорија се врши во ADD CATEGORY делот, којшто е составен од една лабела, едно текст поле и едно копче Add category групирани во една GroupBox контрола. Вработениот го внесува името на категоријата во текст полето и со клик на Add category копчето категоријата се додава во апликацијата (се креира нов запис во dbo.Categories релацијата). Доколку има празен стринг во текст полето, апликацијата го известува вработениот со MessageBox.

####3.4.6 Преглед на комплетираните нарачки (ORDER HISTORY)

Овој дел од AdminForm формата се однесува на преглед на сите комплетирани нарачки (нарачки со статус 3 - испорачани). Се состои од неколку лабели и едно копче View order history групирани во GroupBox контрола. Лабелите ги испишуваат вкупниот број на испорачани нарачки, бројот на навремено испорачани нарачки и бројот на испорачани нарачки со задоцнување. Со клик на копчето View order history, вработениот се пренасочува на нова форма, формата OrderHistoryForm.

#####3.4.G Форма за детален преглед на испорачаните нарачки (OrderHistoryForm)

![Слика 9](https://github.com/Mario172076/Caffe-Bar-VP-/blob/main/CaffeBar/CaffeBar/bin/img/ReservationDetailsForm.png?raw=true "Слика 9: Форма за детален преглед на испорачаните нарачки")

Во принцип оваа форма е иста и како ReceiptForm формата. Се состои од една Label контрола и една DataGridView контрола. Во DataGridView контролата се испишуваат сите нарачки од базата на податоци кои имаат статус 3 (испорачани). Лабелата го дава описот на формата.

Користени контроли:
- Label контрола
- DataGridView контрола

####3.4.7 Дел за одјавување од формата

Делот за одјавување од AdminForm формата е идентичен како и делот за одјавување во CustomerForm формата. Во текст поле е напишано името на вработениот кој е моментално најавен. Со клик на копчето Logout или со исклучување на формата, вработениот се одјавува од апликацијата (во db.Employees релацијата атрибутот loggedIn се сетиран на 0) и се пренасочува во главната форма.

Користени контроли во AdminForm формата:
- GroupBox контроли - за групирање на останатите контроли
- Label контроли - за опис на другите контроли и приказ на бројот на испорачани нарачки
- TextBox контроли - за внесување податоци во апликацијата
- Button контроли - за извршување акции во апликацијата
- ComboBox контроли - за избор на вредност при внес на податоци
- ListBox контроли - за приказ на сите маси, нарачки и резервации


**Изработено од: Марио Младеновиќ 172076 и Андреј Топуков 171069**
