//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//class Program
//{
//    static void Main(string[] args)
//    {
//        try
//        {
//            string strNumberA = Console.ReadLine();
//            string strOperate = Console.ReadLine();
//            string strNumberB = Console.ReadLine();
//            string strResult = "";
//            switch (strOperate)
//            {
//                case "+":
//                    strResult = Convert.ToString(Convert.ToDouble(strNumberA) + Convert.ToDouble(strNumberB));
//                    break;
//                case "-":
//                    strResult = Convert.ToString(Convert.ToDouble(strNumberA) - Convert.ToDouble(strNumberB));
//                    break;
//                case "*":
//                    strResult = Convert.ToString(Convert.ToDouble(strNumberA) * Convert.ToDouble(strNumberB));
//                    break;
//                case "/":
//                    if (strNumberB != "0")
//                        strResult = Convert.ToString(Convert.ToDouble(strNumberA) / Convert.ToDouble(strNumberB));
//                    else
//                        strResult = "除数不能为0";
//                    break;


//            }
//            Console.WriteLine("结果是：" + strResult);
//            Console.ReadLine();
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e.ToString());
//        }
//    }
//}
//using System;

//public class Operation
//{
//    public static double GetResult(double numberA, double numberB,string operate)
//    {
//        double result = 0d;
//        switch(operate)
//        {
//            case "+":
//                result = numberA+numberB;
//                break;
//            case "-":
//                result = numberA - numberB;
//                break;
//            case "*":
//                result = numberA * numberB;
//                break;
//            case "/":
//                result = numberA / numberB;
//                break;
//        }
//        return result;
//    }
//    static void Main(string[] args)
//    {
//        try
//        {
//            string strNumberA = Console.ReadLine();
//            string strOperate = Console.ReadLine();
//            string strNumberB = Console.ReadLine();
//            string strResult = "";
//            strResult = Convert.ToString(Operation.GetResult(Convert.ToDouble(strNumberA), Convert.ToDouble(strNumberB), strOperate));
//            Console.WriteLine("结果是：" + strResult);
//            Console.ReadLine();
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e.Message);
//        }
//    }
//}

//public class Operation
//{
//    private double _numberA = 0;
//    private double _numberB = 0;

//    public double NumberA
//    {
//        get { return _numberA; }
//        set { _numberA = value; }
//    }
//    public double NumberB
//    {
//        get { return _numberB; }
//        set { _numberB = value; }
//    }

//    public virtual double GetResult()
//    {
//        double result = 0;
//        return result;
//    }
//}

//class OperationAdd:Operation//继承，派生类
//{
//    public override double GetResult()//虚方法
//    {
//        double result = 0;
//        result=NumberA + NumberB;
//        return result;
//    }
//}

//class OperationSub:Operation
//{
//    public override double GetResult()
//    {
//        double result = 0;
//        result=NumberA - NumberB;
//        return result;
//    }
//}

//class OperationMul:Operation
//{
//    public override double GetResult()
//    {
//        double result = 0;
//        result=NumberA * NumberB;
//        return result;
//    }
//}

//class OperationDiv:Operation
//{
//    public override double GetResult()
//    {
//        double result = 0;
//        if (NumberB == 0)
//            throw new System.Exception("除数不能为0.");
//        return result;
//    }
//}

//public class OperationFactorty
//{
//    public static Operation createOperate(string operate)
//    {
//        Operation oper = null;
//        switch (operate)
//        {
//            case "+":
//                oper = new OperationAdd();
//                break;
//            case "-":
//                oper = new OperationSub();
//                break;
//            case "*":
//                oper = new OperationMul();
//                break;
//            case "/":
//                oper = new OperationDiv();
//                break;
//        }
//        return oper;
//    }
//}






//using System;

//double total = 0.0d;

//void btnOK_Click(object sender,EventArgs e)
//{
//    double totalPrices=Convert.ToDouble(txtPrice.Text)*Convert.ToDouble(txtNum.Text);
//    total = total + totalPrices;

//    lbxList.Items.Add("单价：" + txtPrice.Text + "数量:" + txtNum.Text + "合计：" + totalPrices.ToString());
//    lblResult.Text=total.ToString();
//}

//using System;

//double total = 0.0d;
//void From1_Loda(object sender,EventArgs e)
//{
//    cbxType.Items.AddRange(new object[] { "正常收费", "打八折", "打七折", "打五折" });
//    cbxType.SelectedIndex = 0;
//}

//void btnOk_cilck(object sender, EventArgs e)
//{
//    double totalPrices = 0d;
//    switch(cbxType.SelectedIndex)
//    {
//        case 0:
//            totalPrices=Convert.ToDouble(txtPrice.Text)*Convert.ToDouble(txtNum.Text); 
//            break;
//        case 1:
//            totalPrices = Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(txtNum.Text)*0.8;
//            break ;
//        case 2:
//            totalPrices = Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(txtNum.Text)*0.7;
//            break ;
//        case 3:
//            totalPrices = Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(txtNum.Text)0.5;
//            break ;

//    }
//    total += totalPrices;
//    lbxList.Items.Add("单价：" + txtPrice.Text + "数量:" + txtNum.Text + "合计：" + totalPrices.ToString());
//    lblResult.Text=total.ToString();
//}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.U2D.Animation;

abstract class CashSuper
{
    public abstract double acceptCash(double money);
}

class CashNormal : CashSuper
{
    public override double acceptCash(double money)
    {
        return money;
    }
}

class CashRebate : CashSuper
{
    public double moneyRebate = 1d;
    public CashRebate(string moneyRebate)
    {
        this.moneyRebate = double.Parse(moneyRebate);
    }

    public override double acceptCash(double money)
    {
        return money * moneyRebate;
    }
}

class CashRetrun : CashSuper
{
    private double moneyCondition = 0.0d;
    private double moneyReturn = 0.0d;
    public CashRetrun(string moneyCondition, string moneyReturn)
    {
        this.moneyCondition = double.Parse(moneyCondition);
        this.moneyReturn = double.Parse(moneyReturn);
    }
    public override double acceptCash(double money)
    {
        double result = money;
        if (money >= moneyCondition)
            result = money - Math.Floor(money / moneyCondition) * moneyReturn;

        return result;
    }

}

class CashFactory
{
    public static CashSuper createCashAccept(string type)
    {
        CashSuper cs = null;
        switch (type)
        {
            case "正常收费":
                cs = new CashNormal();
                break;
            case "满300返100":
                CashRetrun cr1 = new CashRetrun("300", "100");
                cs = cr1;
                break;
            case "打8折":
                CashRebate cr2 = new CashRebate("0.8");
                cs = cr2;
                break;
        }
        return cs;
    }
}

////客户端窗口程序主要部分
//double total = 0.0d;
//private void btnOk_Click(object sendewr,EventArgs e)
//{
//    CashSuper super = CashFactory.createCashAccept(cbxType.SelectItem.Tostring());
//    double totalPrices = 0d;
//    totalPrices = CashSuper.acceptCash(Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(txtNum.Text));
//    total = total + totalPrices;
//    lbxList.Items.Add("单价：" + txtPrice.Text + "数量：" + txtNum.Text + " " + cbxType.SelectedItem + "合计：" + totalPrices.ToString());
//    lblResult.Text=total.ToString();
//}



//class CashContext
//{
//    CashSuper cs = null;

//    public CashContext(string type)
//    {
//        switch (type)
//        {
//            case "正常收费":
//                CashNormal cs0 = new CashNormal();
//                cs = cs0;
//                break;
//            case "满300返100":
//                CashRetrun cr1 = new CashRetrun("300", "100");
//                cs = cr1;
//                break;
//            case "打8折":
//                CashRebate cr2 = new CashRebate("0.8");
//                cs = cr2;
//                break;
//        }
//    }

//    public double GetResult(double money)
//    {
//        return cs.acceptCash(money);
//    }
//}

//抽象类
abstract class Component
{
    //抽象方法
    public abstract void Operation();
}

//具体组件类
class ConcreteComponent : Component
{
    //具体方法
    public override void Operation()
    {
        Console.WriteLine("具体对象的操作");
    }
}

////抽象装饰类
//abstract class Decorator : Component
//{
//    protected Component component;//需要被装饰的组件对象

//    public void SetComponent(Component component)
//    {
//        this.component = component;
//    }

//    public override void Operation()
//    {
//        if (component != null)
//        {
//            component.Operation();
//        }
//    }
//}

//class ConcreteDecoratorA : Decorator
//{
//    private string addedState;

//    public override void Operation()
//    {
//        base.Operation();
//        addedState = "New State";
//        Console.WriteLine("具体装饰对象A的操作");
//    }
//}

//class ConcreteDecoratorB : Decorator
//{
//    public override void Operation()
//    {
//        base.Operation();
//        AddedBehavior();
//        Console.WriteLine("具体装饰对象B的操作");
//    }

//    private void AddedBehavior()
//    {

//    }
//}

//static void Main(string[] args)
//{
//    ConcreteComponent c= new ConcreteComponent();
//    ConcreteDecoratorA d1 = new ConcreteDecoratorA();
//    ConcreteDecoratorB d2 = new ConcreteDecoratorB();

//    d1.SetComponent(c);
//    d2.SetComponent(d1);
//    d2.Operation();

//    Console.Read();
//}

//class Person
//{
//    public Person()
//    { }

//    private string name;
//    public Person(string name)
//    {
//        this.name = name;
//    }

//    public virtual void Show()
//    {
//        Console.WriteLine("装扮的{0}", name);
//    }
//}

//class Finery : Person
//{
//    protected Person component;

//    public void Decorate(Person component)
//    {
//        this.component = component;
//    }

//    public override void Show()
//    {
//        if (component != null)
//        {
//            component.Show();
//        }
//    }
//}

//class TShirts : Finery
//{
//    public override void Show()
//    {
//        Console.Write("大T恤");
//        base.Show();
//    }
//}

//class BigTrouser : Finery
//{
//    public override void Show()
//    {
//        Console.Write("垮裤");
//        base.Show();
//    }
//}

//static void Main(string[] args)
//{
//    Person xc = new Person("小菜");

//    Console.WriteLine("\n 第一种装扮：");

//    Sneakers pqx = new Sneakers();
//    BigTrouser kk = new BigTrouser();
//    TShirts dtx = new TShirts();

//    pqx.Decorate(xc);
//    kk.Decorate(pqx);
//    dtx.Decorate(kk);
//    dtx.Show();

//    Console.WriteLine("\n 第二种装扮：");

//    LeatherShoes px = new LeatherShoes();
//    Tie ld = new Tie();
//    Suit xz = new Suit();

//    px.Decorate(xc);
//    ld.Decorate(px);
//    xz.Decorate(ld);
//    xz.Show();

//    Console.Read();
//}

//abstract class Subject
//{
//    public abstract void Request();
//}

//class RealSubject : Subject
//{
//    public override void Request()
//    {
//        Console.WriteLine("真实的请求");
//    }
//}

//class Proxy : Subject
//{
//    RealSubject realSubject;

//    public override void Request()
//    {
//        if (realSubject == null)
//        {
//            realSubject = new RealSubject();
//        }
//        realSubject.Request();
//    }
//}

//static void Main(string[] args)
//{
//    Proxy proxy = new Proxy();
//    proxy.Request();

//    Console.Read();
//}


//interface GiveGift
//{
//    void GiveDolls();
//    void GiveFlowers();
//    void GiveChocolate();
//}

////追求者类
//class Pursuit : GiveGift
//{
//    SchoolGirl mm;
//    public Pursuit(SchoolGirl mm)
//    {
//        this.mm = mm;
//    }

//    public void GiveDolls()
//    {
//        Console.WriteLine(mm.Name + " 送你洋娃娃");
//    }

//    public void GiveFlowers()
//    {
//        Console.WriteLine(mm.Name + " 送你鲜花");
//    }

//    public void GiveChocolate()
//    {
//        Console.WriteLine(mm.Name + " 送你巧克力");
//    }
//}

////代理类
//class Proxy : GiveGift
//{
//    Pursuit gg;
//    public Proxy(SchoolGirl mm)
//    {
//        gg = new Pursuit(mm);
//    }

//    public void GiveDolls()
//    {
//        gg.GiveDolls();
//    }

//    public void GiveFlowers()
//    {
//        gg.GiveFlowers();
//    }

//    public void GiveChocolate()
//    {
//        gg.GiveChocolate();
//    }
//}

//static void Main(string[] args)
//{
//    SchoolGirl jiaojiao = new SchoolGirl();
//    jiaojiao.Name = "李娇娇";

//    Proxy daili = new Proxy(jiaojiao);

//    daili.GiveDolls();
//    daili.GiveFlowers();
//    daili.GiveChocolate();

//    Console.Read();
//}

//interface IFactory
//{
//    LeiFeng CreateLeiFeng();
//}


////大学生工厂
//class UndergraduateFactory : IFactory
//{
//    public LeiFeng CreateLeiFeng()
//    {
//        return new Undergraduate();
//    }
//}
////志愿者工厂
//class VolunteerFactory : IFactory
//{
//    public LeiFeng CreateLeiFeng()
//    {
//        return new Volunteer();
//    }
//}

//客户端
//IFactory factory = new UndergraduateFactory();
//LeiFeng student = factory.CreateLeiFeng();

//student.BuyRice();
//student.Sweep();
//student.Wash();


////简历类
//class Resume : ICloneable
//{
//    private string name;
//    private string sex;
//    private string age;
//    private string timeArea;
//    private string company;

//    public Resume(string name)
//    {
//        this.name = name;
//    }

//    // 设置个人信息
//    public void SetPersonalInfo(string sex, string age)
//    {
//        this.sex = sex;
//        this.age = age;
//    }

//    // 设置工作经历
//    public void SetWorkExperience(string timeArea, string company)
//    {
//        this.timeArea = timeArea;
//        this.company = company;
//    }

//    // 显示
//    public void Display()
//    {
//        Console.WriteLine("{0} {1} {2}", name, sex, age);
//        Console.WriteLine("工作经历: {0} {1}", timeArea, company);
//    }

//    // 实现ICloneable接口的Clone方法
//    public object Clone()
//    {
//        return (Object)this.MemberwiseClone();
//    }
//}

////客户端
//static void Main(string[] args)
//{
//    Resume a = new Resume("大鸟");
//    a.SetPersonalInfo("男", "29");
//    a.SetWorkExperience("1998-2000", "XX 公司");

//    Resume b = (Resume)a.Clone();
//    b.SetWorkExperience("1998-2006", "YY 企业");

//    Resume c = (Resume)a.Clone();
//    c.SetPersonalInfo("男", "24");

//    a.Display();
//    b.Display();
//    c.Display();

//    Console.Read();
//}

//class WorkExperience
//{
//    private string workDate;
//    public string WorkDate
//    {
//        get { return workDate; }
//        set { workDate = value; }
//    }

//    private string company;
//    public string Company
//    {
//        get { return company; }
//        set { company = value; }
//    }
//}

//class Resume : ICloneable
//{
//    private string name;
//    private string sex;
//    private string age;
//    private WorkExperience work;

//    public Resume(string name)
//    {
//        this.name = name;
//        work = new WorkExperience();
//    }

//    // 设置个人信息
//    public void SetPersonalInfo(string sex, string age)
//    {
//        this.sex = sex;
//        this.age = age;
//    }

//    // 设置工作经历
//    public void SetWorkExperience(string workDate, string company)
//    {
//        work.WorkDate = workDate;
//        work.Company = company;
//    }

//    // 显示
//    public void Display()
//    {
//        Console.WriteLine("{0} {1} {2}", name, sex, age);
//        Console.WriteLine("工作经历: {0} {1}", work.WorkDate, work.Company);
//    }

//    // 实现ICloneable接口的Clone方法
//    public object Clone()
//    {
//        return (Object)this.MemberwiseClone();
//    }
//}

//static void Main(string[] args)
//{
//    Resume a = new Resume("大鸟");
//    a.SetPersonalInfo("男", "29");
//    a.SetWorkExperience("1998-2000", "XX 公司");

//    Resume b = (Resume)a.Clone();
//    b.SetWorkExperience("1998-2006", "YY 企业");

//    Resume c = (Resume)a.Clone();
//    c.SetWorkExperience("1998-2003", "ZZ 企业");

//    a.Display();
//    b.Display();
//    c.Display();

//    Console.Read();
//}

//父类 抽象类
//abstract class AbstractClass
//{
//    public abstract void PrimitiveOperation1();
//    public abstract void PrimitiveOperation2();

//    public void TemplateMethod()
//    {
//        PrimitiveOperation1();
//        PrimitiveOperation2();
//        Console.WriteLine("");
//    }
//}

////子类 具体类A
//class ConcreteClassA : AbstractClass
//{
//    public override void PrimitiveOperation1()
//    {
//        Console.WriteLine("具体类A方法1实现");
//    }

//    public override void PrimitiveOperation2()
//    {
//        Console.WriteLine("具体类A方法2实现");
//    }
//}

////子类 具体类B
//class ConcreteClassB : AbstractClass
//{
//    public override void PrimitiveOperation1()
//    {
//        Console.WriteLine("具体类B方法1实现");
//    }

//    public override void PrimitiveOperation2()
//    {
//        Console.WriteLine("具体类B方法2实现");
//    }
//}

//客户端
//static void Main(string[] args)
//{
//    AbstractClass c;

//    c = new ConcreteClassA();
//    c.TemplateMethod();

//    c = new ConcreteClassB();
//    c.TemplateMethod();

//    Console.Read();
//}

//四个子系统的类
//class SubSystemOne
//{
//    public void MethodOne()
//    {
//        Console.WriteLine(" 子系统方法一");
//    }
//}

//class SubSystemTwo
//{
//    public void MethodTwo()
//    {
//        Console.WriteLine(" 子系统方法二");
//    }
//}

//class SubSystemThree
//{
//    public void MethodThree()
//    {
//        Console.WriteLine(" 子系统方法三");
//    }
//}

//class SubSystemFour
//{
//    public void MethodFour()
//    {
//        Console.WriteLine(" 子系统方法四");
//    }
//}

////外观类
//class Facade
//{
//    SubSystemOne one;
//    SubSystemTwo two;
//    SubSystemThree three;
//    SubSystemFour four;

//    public Facade()
//    {
//        one = new SubSystemOne();
//        two = new SubSystemTwo();
//        three = new SubSystemThree();
//        four = new SubSystemFour();
//    }

//    public void MethodA()
//    {
//        Console.WriteLine("\n方法组 A() ---- ");
//        one.MethodOne();
//        two.MethodTwo();
//        four.MethodFour();
//    }

//    public void MethodB()
//    {
//        Console.WriteLine("\n方法组 B() ---- ");
//        two.MethodTwo();
//        three.MethodThree();
//    }
//}

//客户端
//static void Main(string[] args)
//{
//    Facade facade = new Facade();
//    facade.MethodA();
//    facade.MethodB();

//    Console.Read();
//}

//产品类
//class Product
//{
//    IList<string> parts = new List<string>();

//    public void Add(string part)
//    {
//        parts.Add(part);
//    }

//    public void Show()
//    {
//        Console.WriteLine("\n产品创建 ----");
//        foreach (string part in parts)
//        {
//            Console.WriteLine(part);
//        }
//    }
//}

////抽象建造者类
//abstract class Builder
//{
//    public abstract void BuildPartA();
//    public abstract void BuildPartB();
//    public abstract Product GetResult();
//}

////具体建造者类
//class ConcreteBuilder1 : Builder
//{
//    private Product product = new Product();

//    public override void BuildPartA()
//    {
//        product.Add("部件 A");
//    }

//    public override void BuildPartB()
//    {
//        product.Add("部件 B");
//    }

//    public override Product GetResult()
//    {
//        return product;
//    }
//}

//class ConcreteBuilder2 : Builder
//{
//    private Product product = new Product();

//    public override void BuildPartA()
//    {
//        product.Add("部件 X");
//    }

//    public override void BuildPartB()
//    {
//        product.Add("部件 Y");
//    }

//    public override Product GetResult()
//    {
//        return product;
//    }
//}

////指挥者类
//class Director
//{
//    public void Construct(Builder builder)
//    {
//        builder.BuildPartA();
//        builder.BuildPartB();
//    }
//}

//客户端
//static void Main(string[] args)
//{
//    Director director = new Director();
//    Builder b1 = new ConcreteBuilder1();
//    Builder b2 = new ConcreteBuilder2();

//    director.Construct(b1);
//    Product p1 = b1.GetResult();
//    p1.Show();

//    director.Construct(b2);
//    Product p2 = b2.GetResult();
//    p2.Show();

//    Console.Read();
//}

//抽象主题类
//abstract class Subject
//{
//    private IList<Observer> observers = new List<Observer>();

//    // 增加观察者
//    public void Attach(Observer observer)
//    {
//        observers.Add(observer);
//    }

//    // 移除观察者
//    public void Detach(Observer observer)
//    {
//        observers.Remove(observer);
//    }

//    // 通知
//    public void Notify()
//    {
//        foreach (Observer o in observers)
//        {
//            o.Update();
//        }
//    }
//}

////抽象观察者类
//abstract class Observer
//{
//    public abstract void Update();
//}

////具体观察者类
//class ConcreteObserver : Observer
//{
//    private string name;
//    private string observerState;
//    private ConcreteSubject subject;

//    public ConcreteObserver(ConcreteSubject subject, string name)
//    {
//        this.subject = subject;
//        this.name = name;
//    }

//    public override void Update()
//    {
//        observerState = subject.SubjectState;
//        Console.WriteLine("观察者{0}的新状态是{1}", name, observerState);
//    }

//    public ConcreteSubject Subject
//    {
//        get { return subject; }
//        set { subject = value; }
//    }
//}

////具体主题类
//class ConcreteSubject : Subject
//{
//    private string subjectState;

//    public string SubjectState
//    {
//        get { return subjectState; }
//        set { subjectState = value; }
//    }
//}

//客户端
//static void Main(string[] args)
//{
//    ConcreteSubject s = new ConcreteSubject();

//    s.Attach(new ConcreteObserver(s, "X"));
//    s.Attach(new ConcreteObserver(s, "Y"));

//    s.Attach(new ConcreteObserver(s, "Z"));
//    s.SubjectState = "ABC";
//    s.Notify();

//    Console.Read();
//}

//抽象状态类
//public abstract class State
//{
//    public abstract void WriteProgram(Work w);
//}

////上午工作状态类
//public class ForenoonState : State
//{
//    public override void WriteProgram(Work w)
//    {
//        if (w.Hour < 12)
//        {
//            Console.WriteLine("当前时间: {0}点 上午工作，精神百倍", w.Hour);
//        }
//        else
//        {
//            w.SetState(new NoonState()); w.WriteProgram();
//        }
//    }
//}

////中午工作状态类
//public class NoonState : State
//{
//    public override void WriteProgram(Work w)
//    {
//        if (w.Hour < 13)
//        {
//            Console.WriteLine("当前时间: {0}点 饿了，午饭: 犯困，午休。", w.Hour);
//        }
//        else
//        {
//            w.SetState(new AfternoonState()); w.WriteProgram();
//        }
//    }
//}

////下午工作状态类
//public class AfternoonState : State
//{
//    public override void WriteProgram(Work w)
//    {
//        if (w.Hour < 17)
//        {
//            Console.WriteLine("当前时间: {0}点 下午状态还不错，继续努力", w.Hour);
//        }
//        else
//        {
//            w.SetState(new EveningState()); w.WriteProgram();
//        }
//    }
//}

////晚间工作状态类
//public class EveningState : State
//{
//    public override void WriteProgram(Work w)
//    {
//        if (w.TaskFinished)
//        {
//            w.SetState(new RestState());
//            w.WriteProgram();
//        }
//        else
//        {
//            if (w.Hour < 21)
//            {
//                Console.WriteLine("当前时间: {0}点 加班哦，疲累之极", w.Hour);
//            }
//            else
//            {
//                w.SetState(new SleepingState()); w.WriteProgram();
//            }
//        }
//    }
//}

////睡眠状态类
//public class SleepingState : State
//{
//    public override void WriteProgram(Work w)
//    {
//        Console.WriteLine("当前时间: {0}点不行了，睡着了。", w.Hour);
//    }
//}

////工作类
//public class Work
//{
//    private State current;
//    public Work()
//    {
//        current = new ForenoonState();
//    }

//    private double hour;
//    public double Hour
//    {
//        get { return hour; }
//        set { hour = value; }
//    }

//    private bool finish = false;
//    public bool TaskFinished
//    {
//        get { return finish; }
//        set { finish = value; }
//    }

//    public void SetState(State s)
//    {
//        current = s;
//    }

//    public void WriteProgram()
//    {
//        current.WriteProgram(this);
//    }
//}

//客户端
//static void Main(string[] args)
//{
//    //紧急项目
//    Work emergencyProjects = new Work();
//    emergencyProjects.Hour = 9;
//    emergencyProjects.WriteProgram();
//    emergencyProjects.Hour = 10;
//    emergencyProjects.WriteProgram();
//    emergencyProjects.Hour = 12;
//    emergencyProjects.WriteProgram();
//    emergencyProjects.Hour = 13;
//    emergencyProjects.WriteProgram();
//    emergencyProjects.Hour = 14;
//    emergencyProjects.WriteProgram();
//    emergencyProjects.Hour = 17;

//    //emergencyProjects.WorkFinished = true;
//    emergencyProjects.TaskFinished = false;

//    emergencyProjects.WriteProgram();
//    emergencyProjects.Hour = 19;
//    emergencyProjects.WriteProgram();
//    emergencyProjects.Hour = 22;
//    emergencyProjects.WriteProgram();

//    Console.Read();
//}

// 球员抽象类
//abstract class Player
//{
//    protected string name;
//    public Player(string name)
//    {
//        this.name = name;
//    }

//    public abstract void Attack();
//    public abstract void Defense();
//}

////前锋类
//class Forwards : Player
//{
//    public Forwards(string name) : base(name)
//    {
//    }

//    public override void Attack()
//    {
//        Console.WriteLine("前锋 {0} 进攻", name);
//    }

//    public override void Defense()
//    {
//        Console.WriteLine("前锋 {0} 防守", name);
//    }
//}

//// 中锋
//class Center : Player
//{
//    public Center(string name) : base(name)
//    {
//    }

//    public override void Attack()
//    {
//        Console.WriteLine("中锋 {0} 进攻", name);
//    }

//    public override void Defense()
//    {
//        Console.WriteLine("中锋 {0} 防守", name);
//    }
//}

//// 后卫
//class Guards : Player
//{
//    public Guards(string name) : base(name)
//    {
//    }

//    public override void Attack()
//    {
//        Console.WriteLine("后卫 {0} 进攻", name);
//    }

//    public override void Defense()
//    {
//        Console.WriteLine("后卫 {0} 防守", name);
//    }
//}

//// 外籍中锋
//class ForeignCenter
//{
//    private string name;
//    public string Name
//    {
//        get { return name; }
//        set { name = value; }
//    }

//    public void 进攻()
//    {
//        Console.WriteLine("外籍中锋 {0} 进攻", name);
//    }

//    public void 防守()
//    {
//        Console.WriteLine("外籍中锋 {0} 防守", name);
//    }
//}

//// 翻译者
//class Translator : Player
//{
//    private ForeignCenter wjzf = new ForeignCenter();

//    public Translator(string name)
//        : base(name)
//    {
//        wjzf.Name = name;
//    }

//    public override void Attack()
//    {
//        wjzf.进攻();
//    }

//    public override void Defense()
//    {
//        wjzf.防守();
//    }
//}

//客户端
//static void Main(string[] args)
//{
//    Player b = new Forwards("巴蒂尔");
//    b.Attack();

//    Player m = new Guards("麦克格雷迪");
//    m.Attack();

//    Player ym = new Translator("姚明");
//    ym.Attack();
//    ym.Defense();

//    Console.Read();
//}

//发起人类
//class Originator
//{
//    private string state;
//    public string State
//    {
//        get { return state; }
//        set { state = value; }
//    }

//    public Memento CreateMemento()//创建备忘录，把当前需要保存信息实例化
//    {
//        return (new Memento(state));
//    }

//    public void SetMemento(Memento memento)//把备忘录里的数据拿出来
//    {
//        state = memento.State;
//    }

//    public void Show()
//    {
//        Console.WriteLine("State=" + state);
//    }
//}

////备忘录类
//class Memento
//{
//    private string state;

//    public Memento(string state)
//    {
//        this.state = state;
//    }

//    public string State
//    {
//        get { return state; }
//    }
//}

////管理者类
//class Caretaker
//{
//    private Memento memento;

//    public Memento Memento
//    {
//        get { return memento; }
//        set { memento = value; }
//    }
//}

//客户端
//static void Main(string[] args)
//{
//    Originator o = new Originator();
//    o.State = "On";
//    o.Show();

//    Caretaker c = new Caretaker();
//    c.Memento = o.CreateMemento();
//    o.State = "Off";
//    o.Show();

//    o.SetMemento(c.Memento);
//    o.Show();

//    Console.Read();
//}

//公司抽象类
//abstract class Company
//{
//    protected string name;

//    public Company(string name)
//    {
//        this.name = name;
//    }

//    public abstract void Add(Company c);
//    public abstract void Remove(Company c);
//    public abstract void Display(int depth);
//    public abstract void LineOfDuty();//履行职责
//}

////公司具体类
//class ConcreteCompany : Company
//{
//    private List<Company> children = new List<Company>();

//    public ConcreteCompany(string name)
//        : base(name)
//    { }

//    public override void Add(Company c)
//    {
//        children.Add(c);
//    }
//    public override void Remove(Company c)
//    {
//        children.Remove(c);
//    }
//    public override void Display(int depth)
//    {
//        Console.WriteLine(new String('-', depth) + name);

//        foreach(Company component in children)
//        {
//            component.Display(depth+2);
//        }
//    }

//    public override void LineOfDuty()
//    {
//        foreach(Company component in children)
//        {
//            component.LineOfDuty();
//        }
//    }
//}

////人力资源部和财务部类 当做是树叶节点
//class HRDepartment : Company
//{
//    public HRDepartment(string name) : base(name)
//    { }

//    public override void Add(Company c)
//    { }

//    public override void Remove(Company c)
//    { }

//    public override void Display(int depth)
//    {
//        Console.WriteLine(new String('-', depth) + name);
//    }
//    public override void LineOfDuty()
//    {
//        Console.WriteLine("{0} 员工招聘培训管理", name);
//    }
//}
//class FinanceDepartment : Company
//{
//    public FinanceDepartment(string name) : base(name)
//    { }

//    public override void Add(Company c)
//    { }

//    public override void Remove(Company c)
//    { }

//    public override void Display(int depth)
//    {
//        Console.WriteLine(new String('-', depth) + name);
//    }

//    public override void LineOfDuty()
//    {
//        Console.WriteLine("{0} 公司财务收支管理", name);
//    }
//}

//static void Main(string[] args)
//{
//    ConcreteCompany root = new ConcreteCompany("广东基础公司");
//    root.Add(new SubDepartment("信息公司人力资源部"));
//    root.Add(new SubDepartment("信息公司技术部"));
//    root.Add(new ConcreteCompany("上海新新分公司"));
//    root.Add(new SubDepartment("市场开发公司人力资源部"));
//    root.Add(new SubDepartment("市场开发公司技术部"));

//    ConcreteCompany comp1 = new ConcreteCompany("信息分公司");
//    comp1.Add(new SubDepartment("信息公司人力资源部"));
//    comp1.Add(root);

//    ConcreteCompany comp2 = new ConcreteCompany("市场分公司");
//    comp2.Add(new SubDepartment("市场开发公司技术部"));
//    comp2.Add(comp1);

//    Console.WriteLine("公司数量: ");
//    root.ListCompany();

//    Console.ReadLine();
//}

//迭代抽象类
//abstract class Iterator
//{
//    public abstract object First();
//    public abstract object Next();
//    public abstract bool IsDone();
//    public abstract object CurrentItem();
//}

////聚集抽象类
//abstract class Aggregate
//{
//    public abstract Iterator CreateIterator();
//}

////具体迭代器类
//class ConcreteIterator : Iterator
//{
//    private ConcreteAggregate aggregate;
//    private int current = 0;

//    public ConcreteIterator(ConcreteAggregate aggregate)
//    {
//        this.aggregate = aggregate;
//    }

//    public override object First()
//    {
//        return aggregate[0];
//    }

//    public override object Next()
//    {
//        object ret = null;
//        current++;
//        if (current < aggregate.Count)
//        {
//            ret = aggregate[current];
//        }
//        return ret;
//    }

//    public override bool IsDone()
//    {
//        return current >= aggregate.Count ? true : false;
//    }

//    public override object CurrentItem()
//    {
//        return aggregate[current];
//    }
//}

////具体聚集类
//class ConcreteAggregate : Aggregate
//{
//    private IList<object> items = new List<object>();

//    public override Iterator CreateIterator()
//    {
//        return new ConcreteIterator(this);
//    }

//    public int Count
//    {
//        get { return items.Count; }
//    }

//    public object this[int index]
//    {
//        get { return items[index]; }
//        set { items.Insert(index, value); }
//    }
//}

//客户端
//static void Main(string[] args)
//{
//    ConcreteAggregate a = new ConcreteAggregate();
//    a[0] = "大鸟";
//    a[1] = "小菜";
//    a[2] = "行李";
//    a[3] = "老外";
//    a[4] = "公交内部员工";
//    a[5] = "小偷";

//    Iterator i = new ConcreteIterator(a);

//    object item = i.First();
//    while (!i.IsDone())
//    {
//        Console.WriteLine("{0} 请买车票！", i.CurrentItem());
//        i.Next();
//    }
//    Console.Read();
//}

//手机软件
//abstract class HandsetSoft
//{
//    public abstract void Run();
//}

////手机通讯录
//class HandsetAddressList : HandsetSoft
//{
//    public override void Run()
//    {
//        Console.WriteLine("运行手机通讯录");
//    }
//}

////手机游戏
//class HandsetGame : HandsetSoft
//{
//    public override void Run()
//    {
//        Console.WriteLine("运行手机游戏");
//    }
//}

////手机品牌
//abstract class HandsetBrand
//{
//    protected HandsetSoft soft;

//    //设置手机软件
//    public void SetHandsetSoft(HandsetSoft soft)
//    {
//        this.soft = soft;
//    }

//    public abstract void Run();
//}

////手机品牌N
//class HandsetBrandN : HandsetBrand
//{
//    public override void Run()
//    {
//        soft.Run();
//    }
//}

////手机品牌M
//class HandsetBrandM : HandsetBrand
//{
//    public override void Run()
//    {
//        soft.Run();
//    }
//}

//客户端
//static void Main(string[] args)
//{
//    HandsetBrand ab;
//    ab= new HandsetBrandN();

//    ab.SetHandsetSoft(new HandsetGame());
//    ab.Run();

//    ab.SetHandsetSoft(new HandsetAddressList());
//    ab.Run();

//    ab = new HandsetBrandM();

//    ab.SetHandsetSoft(new HandsetGame());
//    ab.Run();

//    ab.SetHandsetSoft(new HandsetAddressList());
//    ab.Run();

//    Console.Read();
//}

//abstract class Implementor
//{
//    public abstract void Operation();
//}

//class ConcreteImplentorA : Implementor
//{
//    public override void Operation()
//    {
//        Console.WriteLine("具体实现A的方法执行");
//    }
//}

//class ConcreteImplementorB: Implementor
//{
//    public override void Operation()
//    {
//        Console.WriteLine("具体实现B的方法执行");
//    }
//}

//class Abstraction
//{
//    protected Implementor implementor;

//    public void SetImplementor(Implementor implementor)
//    {
//        this.implementor = implementor;
//    }

//    public virtual void Operation()
//    {
//        implementor.Operation();
//    }
//}

//class RefinedAbstraction : Abstraction
//{
//    public override void Operation()
//    {
//        implementor.Operation();
//    }
//}

//客户端
//static void Main(string[] args)
//{
//    Abstraction ab = new RefinedAbstraction();

//    ab.SetImplementor(new ConcreteImplementorA());
//    ab.Operation();

//    ab.SetImplementor(new ConcreteImplementorB());
//    ab.Operation();

//    Console.Read();
//}

//烤羊肉串者
//public class Barbecuer
//{
//    public void BakeMutton()
//    {
//        Console.WriteLine("烤羊肉串！");
//    }

//    public void BakeChickenWing()
//    {
//        Console.WriteLine("烤鸡翅");
//    }
//}

////抽象命令类
//public abstract class Command
//{
//    protected Barbecuer receiver;

//    public Command(Barbecuer receiver)
//    {
//        this.receiver = receiver;
//    }//确定烤肉者是谁

//    abstract public void ExcuteCommand();
//}

////具体命令类
//class BakeMuttonCommand : Command
//{
//    public BakeMuttonCommand(Barbecuer receiver)
//        : base(receiver)
//    { }

//    public override void ExcuteCommand()
//    {
//        receiver.BakeMutton();
//    }
//}

//class BakeChickenWingCommand : Command
//{
//    public BakeChickenWingCommand(Barbecuer receiver)
//        : base(receiver) { }

//    public override void ExcuteCommand()
//    {
//        receiver.BakeChickenWing();
//    }
//}

////服务员类
//public class Waiter
//{
//    private IList<Command> orders = new List<Command>();

//    public void SetOrder(Command command)
//    {
//        if(command.ToString()=="命令模式.BakeChickenWingCommand")
//        {
//            Console.WriteLine("服务员：鸡翅没有了，请点别的烧烤。");
//        }
//        else
//        {
//            orders.Add(command);
//            Console.WriteLine("增加订单：" + command.ToString() + "时间：" + DateTime.Now.ToString());
//        }
//    }

//    public void CancelOrder(Command command)
//    {
//        orders.Remove(command);
//        Console.WriteLine("取消订单："+command.ToString()+"时间："+DateTime.Now.ToString());
//    }

//    public void Notify()
//    {
//        foreach(Command cmd in orders)
//        {
//            cmd.ExcuteCommand();
//        }
//    }
//}

//客户端
//static void Main(string[] args)
//{
//    Barbecuer boy = new Barbecuer();
//    Command bakeMuttonCommand1 = new BakeMuttonCommand(boy);
//    Command bakeMuttonCommand2 = new BakeMuttonCommand(boy);
//    Command bakeChickenWingCommand1 = new BakeChickenWingCommand(boy);
//    Waiter girl = new Waiter();

//    girl.SetOrder(bakeMuttonCommand1);
//    girl.SetOrder(bakeMuttonCommand2);
//    girl.SetOrder(bakeChickenWingCommand1);
//    girl.Notify();

//    Consloe.Rend();
//}

//申请
//class Request
//{
//    private string requestType;
//    public string RequestType
//    {
//        get { return requestType; }
//        set { requestType = value; }
//    }

//    private string requestContent;
//    public string RequestContent
//    { 
//        get { return requestContent; } 
//        set { requestContent = value; } 
//    }

//    private int number;
//    public int Number
//    {
//        get { return number; }
//        set { number = value; }
//    }
//}

////管理者
//abstract class Manager
//{
//    protected string name;

//    protected Manager superior;

//    public Manager(string name)
//    {
//        this.name = name;
//    }

//    //管理者上级
//    public void SetSup(Manager superior)
//    {
//        this.superior = superior;
//    }

//    abstract public void RequestApp(Request request);
//}


////经理
//class CommmonManager :Manager
//{
//    public CommmonManager(string name)
//        : base(name)
//    { }
//    public override void RequestApp(Request request)
//    {
//        if(request.RequestType=="请假"&&request.Number<=2)
//        {
//            Console.WriteLine("{0}:{1} 数量{2} 被批准", name, request.RequestContent, request.Number);
//        }
//        else
//        {
//            if(superior!=null)
//                superior.RequestApp(request);
//        }
//    }
//}

////总监
//class Majordomo : Manager
//{
//    public Majordomo(string name)
//    : base(name) { }
//    public override void RequestApp(Request request)
//    {
//        if (request.RequestType == "请假" && request.Number <= 5)
//        {
//            Console.WriteLine("{0}:{1} 数量{2} 被批准", name, request.RequestContent, request.Number);
//        }
//        else
//        {
//            if(superior!=null)
//                superior.RequestApp(request);
//        }
//    }
//}

////总经理
//class GeneralManager :Manager
//{
//    public GeneralManager(string name)
//   : base(name) { }
//    public override void RequestApp(Request request)
//    {
//        if (request.RequestType == "请假")
//        {
//            Console.WriteLine("{0}:{1} 数量{2} 被批准", name, request.RequestContent, request.Number);
//        }
//        else if (request.RequestType == "加薪" && request.Number <= 500)
//        {
//            Console.WriteLine("{0}:{1} 数量{2} 被批准", name, request.RequestContent, request.Number);
//        }
//        else if (request.RequestType == "加薪" && request.Number > 500)
//        {
//            Console.WriteLine("{0}:{1} 数量{2} 再说吧",name,request.RequestContent,request.Number);
//        } 

//    }
//}

//客户端
//static void Main(string[] args)
//{
//    CommmonManager jinli = new CommmonManager("景");
//    Majordomo zongjian = new Majordomo("总");
//    GeneralManager zongjingli = new GeneralManager("钟经理");
//    jinli.SetSup(zongjian);
//    zongjian.SetSup(zongjingli);

//    Request request = new Request();
//    request.RequestType = "请假";
//    request.Number = 1;
//    request.RequestContent = "小菜请假";
//    jinli.RequestApp(request);

//    Request request1 = new Request();
//    request1.RequestType = "请假";
//    request1.Number = 4;
//    request1.RequestContent = "小菜请假";
//    jinli.RequestApp(request2);

//    Request request2 = new Request();
//    request2.RequestType = "加薪";
//    request.Number = 500;
//    request2.RequestContent = "小菜加薪";
//    jinli.RequestApp(request2);

//    Request request3 = new Request();
//    request3.RequestType = "加薪";
//    request3umber = 1000;
//    request3RequestContent = "小菜加薪";
//    jinli.RequestApp(request3);

//    Console.Read();

//}

//联合国机构
//abstract class UnitedNations
//{
//    public abstract void Declare(string message, Country collrague);
//}

////国家
//abstract class Country
//{
//    protected UnitedNations mediator;

//    public Country(UnitedNations mediator)
//    {
//        this.mediator = mediator;
//    }
//}

////美国类
//class USA : Country
//{
//    public USA(UnitedNations mediator) : base(mediator)
//    { }

//    public void Declare(string message)
//    {
//        mediator.Declare(message, this);
//    }
//    public void GetMessage(string message)
//    {
//        Console.WriteLine("美国获得对方信息：" + message);
//    }
//}

////伊拉克
//class  Iraq:Country
//{
//    public Iraq(UnitedNations mediator) : base(mediator)
//    { }

//    public void Declare(string message)
//    {
//        mediator.Declare(message, this);
//    }

//    public void GetMessage(string message)
//    {
//        Console.WriteLine("伊拉克获得对方消息："+message);
//    }
//}

////联合国安全理事会
//class UNSC : UnitedNations
//{
//    private USA usa;
//    private Iraq iraq;

//    public USA cusa
//    {
//        set { usa = value; }
//    }

//    public Iraq ciraq
//    {
//        set { iraq = value; }
//    }

//    public override void Declare(string message, Country collrague)
//    {
//        if(collrague==usa)
//        {
//            iraq.GetMessage(message);
//        }
//        else
//        {
//            usa.GetMessage(message);
//        }
//    }
//}

//客户端
//static void Main(string[] args)
//{
//    UNSC unsc = new UNSC();
//    USA c1 = new USA(UNSC);
//    Iraq c2 = new Iraq(UNSC);

//    UNSC.cusa(c1);
//    UNSC.ciraq(c2);

//    c1.Declare("不准研制核武器，否则要发动战争");
//    c2.Declare("我们没有核武器，也不怕侵略");

//    Console.Read();

//}


//用户
//public class User
//{
//    private string name;
//    public User(string name)
//    {
//        this.name = name;
//    }

//    public string Name
//    {
//        get { return name; } 
//    }
//}


////网站抽象类
//abstract class WebSite
//{
//    public abstract void Use(User user);//要传递用户对象
//}

////具体网站类
//class ConcreteWebSite : WebSite
//{
//    private string name = "";
//    public ConcreteWebSite(string name)
//    {
//        this.name = name;
//    }
//    public override void Use(User user)
//    {
//        Console.WriteLine("网站分类：" + name+"用户："+user.Name);
//    }
//}

////网站工厂
//class WebSiteFactory
//{
//    private Hashtable flyw = new Hashtable();

//    public WebSite GetWebSiteCategory(string key)
//    {
//        if (!flyw.ContainsKey(key))
//            flyw.Add(key, new Hashtable());
//        return ((WebSite)flyw[key]);
//    }

//    public int GetWebSiteCount()
//    {
//        return flyw.Count;
//    }
//}

//客户端
//static void Main(string[] args)
//{
//    WebSiteFactory f=new WebSiteFactory();

//    WebSite fx = f.GetWebSiteCategory("产品展示");
//    fx.Use(new User("小菜"));

//    WebSite fy = f.GetWebSiteCategory("产品展示");
//    fy.Use(new User("大鸟"));

//    WebSite fz = f.GetWebSiteCategory("产品展示");
//    fz.Use(new User("娇娇"));

//    WebSite f1 = f.GetWebSiteCategory("博客");
//    f1.Use(new User("a"));

//    WebSite f2 = f.GetWebSiteCategory("博客");
//    f2.Use(new User("b"));

//    Console.WriteLine("得到网站分类总数为{0}",f.GetWebSiteCount());

//    Console.Read();
//}

//演奏内容类
//class PlayContext
//{
//    private string text;
//    public string PlayText
//    {
//        get { return text; }
//        set { text = value; }
//    }
//}


////表达式类
//abstract class Expression
//{
//    public void Interpret(PlayContext context)
//    {
//        if(context.PlayText.Length==0)
//        {
//            return;
//        }
//        else
//        {
//            string playKey =context.PlayText.Substring(0,1);
//            //取前面两个作为键
//            context.PlayText = context.PlayText.Substring(2);
//            double playValue = Convert.ToDouble(context.PlayText.Substring(0,context.PlayText.IndexOf("")));
//            //将内容里从第一个到出现空字符串的这部分赋值给playvalue，并且是double类型
//            context.PlayText = context.PlayText.Substring(context.PlayText.IndexOf("") + 1);

//            Excute(playKey, playValue);
//        }
//    }

//    public abstract void Excute(string key, double value);
//}

////音符类
//class Note : Expression
//{
//    public override void Excute(string key, double value)
//    {
//        string note = "";
//        switch (key)
//        {
//            case "C":
//                note = "1";
//                break;
//            case "D":
//                note = "2";
//                break;
//            case "E":
//                note = "3";
//                break;
//            case "F":
//                note = "4";
//                break;
//            case "G":
//                note = "5";
//                break;
//            case "A":
//                note = "6";
//                break;
//            case "B":
//                note = "7";
//                break;
//        }
//        Console.Write("{0} ", note);
//    }
//}


////音符类
//class Scale : Expression
//{
//    public override void Excute(string key, double value)
//    {
//        string scale = "";
//        switch (Convert.ToInt32(value))
//        {
//            case 1:
//                scale = "低音";
//                break;
//            case 2:
//                scale = "中音";
//                break;
//            case 3:
//                scale = "高音";
//                break;
//        }
//        Console.Write("{0} ", scale);
//    }
//}


//客户端
//static void Main(string[] args)
//{
//    PlayContext context = new PlayContext();
//    Console.WriteLine("上海滩：");
//    context.PlayText = " O 2 E 0.5 G 0.5 A 3 E 0.5 G 0.5 D 3 E 0.5 G 0.5 A 0.5 O 3 C 1 O 2 A 0.5 G 1 C 0.5 E 0.5 D 3 ";
//    Expression expression = null;
//    try
//    {
//        while (context.PlayText.Length > 0)
//        {
//            string str = context.PlayText.Substring(0, 1);
//            switch (str)
//            {
//                case "O":
//                    expression = new Scale();
//                    break;
//                case "C":
//                case "D":
//                case "E":
//                case "F":
//                case "G":
//                case "A":
//                case "B":
//                case "P":
//                    expression = new Note();
//                    break;
//            }
//            expression.Interpret(context);
//        }
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.Message);
//    }
//    Console.Read();
//}

//Action 和Person抽象类
//abstract class Action
//{
//    // 得到男人结论或反应
//    public abstract void GetManConclusion(Man concreteElementA);
//    // 得到女人结论或反应
//    public abstract void GetWomanConclusion(Woman concreteElementB);
//}

//abstract class Person
//{
//    // 接受
//    public abstract void Accept(Action visitor);
//}

////成功
//class Success : Action
//{
//    public override void GetManConclusion(Man concreteElementA)
//    {
//        Console.WriteLine("{0}{1}时，背后多半有一个伟大的女人。",
//            concreteElementA.GetType().Name, this.GetType().Name);
//    }

//    public override void GetWomanConclusion(Woman concreteElementB)
//    {
//        Console.WriteLine("{0}{1}时，背后大多有一个不成功的男人。",
//            concreteElementB.GetType().Name, this.GetType().Name);
//    }
//}

////失败
//class Failing : Action
//{
//    public override void GetManConclusion(Man concreteElementA)
//    {
//        Console.WriteLine("{0}{1}时，闷头喝酒，谁也不用劝。",
//            concreteElementA.GetType().Name, this.GetType().Name);
//    }

//    public override void GetWomanConclusion(Woman concreteElementB)
//    {
//        Console.WriteLine("{0}{1}时，眼泪汪汪，谁也劝不了。",
//            concreteElementB.GetType().Name, this.GetType().Name);
//    }
//}

////恋爱
//class Amativeness : Action
//{
//    public override void GetManConclusion(Man concreteElementA)
//    {
//        Console.WriteLine("{0}{1}时，凡事不懂也要装懂。",
//            concreteElementA.GetType().Name, this.GetType().Name);
//    }

//    public override void GetWomanConclusion(Woman concreteElementB)
//    {
//        Console.WriteLine("{0}{1}时，遇事懂也装作不懂。",
//            concreteElementB.GetType().Name, this.GetType().Name);
//    }
//}

////男女类
//class Man : Person
//{
//    public override void Accept(Action visitor)
//    {
//        visitor.GetManConclusion(this);
//    }
//}

//class Woman : Person
//{
//    public override void Accept(Action visitor)
//    {
//        visitor.GetWomanConclusion(this);
//    }
//}

////对象结构类
//class ObjectStructure
//{
//    private IList<Person> elements = new List<Person>();

//    // 增加
//    public void Attach(Person element)
//    {
//        elements.Add(element);
//    }

//    // 移除
//    public void Detach(Person element)
//    {
//        elements.Remove(element);
//    }

//    // 查看显示
//    public void Display(Action visitor)
//    {
//        foreach (Person e in elements)
//        {
//            e.Accept(visitor);
//        }
//    }
//}

////结婚状态类
//class Marriage : Action
//{
//    public override void GetManConclusion(Man concreteElementA)
//    {
//        Console.WriteLine("{0}{1}时，感慨道：恋爱游戏终结时，‘有妻徒刑’遥无期。",
//            concreteElementA.GetType().Name, this.GetType().Name);
//    }

//    public override void GetWomanConclusion(Woman concreteElementB)
//    {
//        Console.WriteLine("{0}{1}时，欣慰曰：爱情长跑路漫漫，婚姻保险保平安。",
//            concreteElementB.GetType().Name, this.GetType().Name);
//    }
//}

//客户端
//static void Main(string[] args)
//{
//    ObjectStructure o = new ObjectStructure();
//    o.Attach(new Man());
//    o.Attach(new Woman());

//    // 成功时的反应
//    Success v1 = new Success();
//    o.Display(v1);

//    // 失败时的反应
//    Failing v2 = new Failing();
//    o.Display(v2);

//    // 恋爱时的反应
//    Amativeness v3 = new Amativeness();
//    o.Display(v3);

//    // 婚姻时的反应
//    Marriage v4 = new Marriage();
//    o.Display(v4);

//    Console.Read();
//}


//用户类 ID和昵称
class User
{
    private int _id; 
    public int ID 
    {
        get { return _id; } 
        set { _id = value; } 
    }

    private string _name; 
    public string Name 
    {
        get { return _name; } 
        set { _name = value; } 
    }
}

//提供接口
interface IUser
{
    void Insert(User user);
    User GetUser(int id);
}

class SqlserverUser : IUser
{
    public void Insert(User user)
    {
        Console.WriteLine("在 SQL Server 中给 User 表增加一条记录");
    }

    public User GetUser(int id)
    {
        Console.WriteLine("在 SQL Server 中根据 ID 得到 User 表一条记录");
        return null;
    }
}

class AccessUser : IUser
{
    public void Insert(User user)
    {
        Console.WriteLine("在 Access 中给 User 表增加一条记录");
    }

    public User GetUser(int id)
    {
        Console.WriteLine("在 Access 中根据 ID 得到 User 表一条记录");
        return null;
    }
}

class Department
{
    private int _id;
    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }

    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
}

//增加部门表
interface IDepartment
{
    void Insert(Department department);
    Department GetDepartment(int id);
}

class SqlserverDepartment : IDepartment
{
    public void Insert(Department department)
    {
        Console.WriteLine("在 SQL Server 中给 Department 表增加一条记录");
    }

    public Department GetDepartment(int id)
    {
        Console.WriteLine("在 SQL Server 中根据 ID 得到 Department 表一条记录");
        return null;
    }
}

class AccessDepartment : IDepartment
{
    public void Insert(Department department)
    {
        Console.WriteLine("在 Access 中给 Department 表增加一条记录");
    }

    public Department GetDepartment(int id)
    {
        Console.WriteLine("在 Access 中根据 ID 得到 Department 表一条记录");
        return null;
    }
}

//工厂类
interface IFactory
{
    IUser CreateUser();
    IDepartment CreateDepartment();
}

//具体工厂类
class SqlServerFactory : IFactory
{
    public IUser CreateUser()
    {
        return new SqlserverUser();
    }

    public IDepartment CreateDepartment()
    {
        return new SqlserverDepartment();
    }
}


class AccessFactory : IFactory
{
    public IUser CreateUser()
    {
        return new AccessUser();
    }

    public IDepartment CreateDepartment()
    {
        return new AccessDepartment();
    }
}

//static void Main(string[] args)
//{
//    User user = new User();
//    Department dept = new Department();

//    IFactory factory = new AccessFactory();
//    IUser iu = factory.CreateUser();
//    iu.Insert(user);
//    iu.GetUser(1);

//    IDepartment id = factory.CreateDepartment();
//    id.Insert(dept);
//    id.GetDepartment(1);

//    Console.Read();
//}