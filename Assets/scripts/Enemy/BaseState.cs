//抽象类
public abstract class BaseState
{
    //找到当前的Enemy让子类可以调用Enemy中的变量和方法
    protected Enemy currentEnemy;
    //进入方法(进入的时候需要知道当前的Enemy是谁，脚本挂载在谁的身上)
    public abstract void OnEnter(Enemy enemy);
    //逻辑更新方法
    public abstract void LogicUpdate();

    //物理逻辑判断方法
    public abstract void PhysicsUpdate();
    //退出方法
    public abstract void OnExit();


}
