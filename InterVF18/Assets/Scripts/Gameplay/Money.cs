public class Money {

    ///// Variables /////
    int _money = 50;

    ///// Singleton /////
    private Money() {
    }

    static private Money singleton;

    static public Money Singleton {
        get {
            if (singleton == null)
                singleton = new Money();
            return singleton;
        }
    }

    ///// Properties /////
    static public int money { get { return Singleton._money; } }

    ///// Add //////
    static public void Add(int amount) {
        Singleton._Add(amount);
    }

    void _Add(int amount) {
        _money += amount;
    }

    ///// Buy //////
    static bool Buy(int amount) {
        return Singleton._Buy(amount);
    }

    bool _Buy(int amount) {
        if (_money >= amount) {
            _money -= amount;
            return true;
        } else
            return false;
    }

    ///// TestBuy //////
    static bool TestBuy(int amount) {
        return Singleton._TestBuy(amount);
    }

    bool _TestBuy(int amount) {
        if (_money >= amount) {
            return true;
        } else
            return false;
    }
}
