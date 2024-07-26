// Simple types
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Text;
using System.Linq.Expressions;

string stringVariable = "This is a string";
int intVariable = 0;
float floatVariable = 0.1F;
double doubleVariable = 0.1;
char charVariable = 'a';
bool boolVariable = true;
int[] intArray = [1,2,3,4,5];
int[,,] dimensionalArray = {{{1,2}}, {{1,2}}, {{1,2}}};

Console.WriteLine("string variable:" + stringVariable);
Console.WriteLine("int variable:" + intVariable);
Console.WriteLine("float variable:" + floatVariable);
Console.WriteLine("double variable:" + doubleVariable);
Console.WriteLine("char variable:" + charVariable);
Console.WriteLine("bool variable:" + boolVariable);

// foreach syntax
foreach(int i in intArray){
    Console.WriteLine("Array member:" + i);
}

bool boolRCValue = true == true ? true : false;
Console.WriteLine("conditional variable:" + boolRCValue);

// switch syntax
switch(intVariable){
    case > 0:
        Console.WriteLine("intVariable case > 0");
        break;
    default:
        Console.WriteLine("default Case");
        break;
}

// do while syntax
do {
    Console.WriteLine("loop statement");
}
while(intVariable < 0);

//String methods && immutable strings
//this has no use is the reference to the same string
string clonedString = (string)stringVariable.Clone();
int comparedResult = stringVariable.CompareTo("This is a string");
Console.WriteLine("string compare:" + comparedResult);
bool containedResult = stringVariable.Contains("is");
Console.WriteLine("containedResult:" + containedResult);
bool endResult = stringVariable.EndsWith("ing");
Console.WriteLine("endResult:" + endResult);
bool equalResult = stringVariable.Equals("t");
string upperResult = stringVariable.ToUpper();
string lowerResult = stringVariable.ToLower();
//insert returns a new string
Console.WriteLine("string insert:" + stringVariable.Insert(stringVariable.Length, " insertion"));
int indexString = stringVariable.IndexOf("ing");
string concatString = string.Concat(stringVariable, stringVariable);
Console.WriteLine("Concat Result:" + concatString);

//Mutable string
StringBuilder stringMutable = new();
stringMutable.Append("This is a string");
Console.WriteLine("Mutable string:" + stringMutable);

// OOP Syntax reference

E sampleClass = new();
//from virtual class in class A implemented in class C
sampleClass.MethodOverload();
//MethodA and MethodB are lost not supported multiple inheritance in C#
//from abstract method in class D
sampleClass.AbstractMethod();
//MethodF from interface implemented in class F
sampleClass.MethodF();
//class extension
sampleClass.MethodExtension();
sampleClass.AttributeValue = 1000;
Console.WriteLine("Getter function:" + sampleClass.AttributeValue);

G sampleClassB = new();
sampleClassB.PartialMethodA();
sampleClassB.PartialMethodB();
// class indexer
Console.WriteLine(sampleClassB[3]);
sampleClassB[3] = 1500;
Console.WriteLine(sampleClassB[3]);

//anonymous type
var anonymous = new {
    memberA = "memberA",
    memberB = "memberB",
    nestedMember = new { nestedA = "A", nestedB = "B"},
    arrayMember = new[] {
       new {one = 1, two = 2},
       new {one = 3, two = 4}
    }
};

Console.WriteLine(anonymous.memberA);
Console.WriteLine(anonymous.memberB);
Console.WriteLine(anonymous.nestedMember);
Console.WriteLine(anonymous.arrayMember[1].one);

//Enums and exceptions
try{
    Console.WriteLine(G.FiveValues.Five);
    throw new Exception("Sample Exception");
}
catch(Exception e){
    Console.WriteLine(e.Message.ToString());
}
finally {
    Console.WriteLine("Finally block");
}
// sample delegate
SampleDelegate sampleDelegate = new(A.MethodA);
sampleDelegate();

//sample publisher && subscriber
SamplePublisher publisher = new();
publisher.MyEvent += eventAction;
publisher.PerformEvent();

//anonymous method
PrintMessage printMessage = delegate(string msg){
    Console.WriteLine(msg);
};

printMessage("Anonymous method");

//expression lambda
int[] numbers = [1,2,3,3,4,3,4];
int count = numbers.Count(x => x == 3);
Console.WriteLine("Total count:" + count);

//statement lambda
List<int> numbersList = [1,2,3,3,4,3,4];
count = numbersList.Count(x => {return x ==3;});
Console.WriteLine("Total count:" + count);

//expression tree, parts: body, parameters, nodetype, type
Func<string, string, string> stringJoins = (str1, str2) => string.Concat(str1, str2);
Expression<Func<string,string,string>> stringJoinExpression = (str1, str2) => string.Concat(str1, str2);
var func = stringJoinExpression.Compile();
var result = func("Hello", "World");
Console.WriteLine(result);
result = stringJoinExpression.Compile()("PartA", "PartB");
Console.WriteLine(result);

static void eventAction(object sender, EventArgs e){
    Console.WriteLine("Event processes");
}
delegate void PrintMessage(string msg);

//delegate
delegate void SampleDelegate();


// Events Publisher declaration
public delegate void EventHandler(object sender, EventArgs e);
public class SamplePublisher{
    public event EventHandler? MyEvent;

    protected virtual void OnEvent(EventArgs e){
        MyEvent?.Invoke(this, e);
    }

    public void PerformEvent() {
        OnEvent(EventArgs.Empty);
    }
}

class A {
    public static void MethodA() {
        Console.WriteLine(MethodBase.GetCurrentMethod()?.Name.ToString());
    }

    public virtual void MethodOverload(){
        Console.WriteLine("This is class A implementation");
    }
}

class B: A {
    public static void MethodB() {
        Console.WriteLine(MethodBase.GetCurrentMethod()?.Name.ToString());
    }
}

class C: B {
    public override void MethodOverload(){
        Console.WriteLine("This is class C implementation virtual functions are multiple inherited");
    }
    public static void MethodC() {
        Console.WriteLine(MethodBase.GetCurrentMethod()?.Name.ToString());
    }
}

abstract class D: C {
    public abstract void AbstractMethod();
}

interface IInterfaceF {
    void MethodF();
}

class E: D, IInterfaceF {
    public int AttributeValue{ get; set;}
    public override void AbstractMethod() {
        Console.WriteLine("Abstract classes are multiple inherited");
        Console.WriteLine(MethodBase.GetCurrentMethod()?.Name.ToString());
    }
    public void MethodF(){
        Console.WriteLine("This is an interface implementation. interfaces are multiple inherited");
    }
}

static class Extension {
    public static void MethodExtension(this E parameter){
        Console.WriteLine(MethodBase.GetCurrentMethod()?.Name.ToString());
    }
}

partial class G:E {
    public enum FiveValues:int {
        One, Two, Three, Four, Five
    }
    public void PartialMethodA(){
        Console.WriteLine(MethodBase.GetCurrentMethod()?.Name.ToString());
    }
}

partial class G:E {
    private int[] days = [1,2,3,4,5,6,7];
    // class indexer
    public int this[int index]{
        get {
            return days[index];
        }
        set {
            days[index] = value;
        }
    }
    public void PartialMethodB(){
        Console.WriteLine(MethodBase.GetCurrentMethod()?.Name.ToString());
    }
}

