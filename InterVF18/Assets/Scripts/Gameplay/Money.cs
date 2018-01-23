public class Money {
    ///// Constants /////
    const int moneyStart = 5000;
    const int priceCamera = 10;
    const int priceCameraIncrease = 1;
    const int priceGuard = 10;
    const int priceGuardIncrease = 1;
    const int priceLaser = 10;
    const int priceLaserIncrease = 1;

    ///// Variables /////
    int _money = moneyStart;
    int _priceCamera = priceCamera;
    int _priceGuard = priceGuard;
    int _priceLaser = priceLaser;

    ///// Singleton /////
    private Money() {
		//MoneyController.Instance.UpdateMoneyOnHUD ();
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
		MoneyController.Instance.UpdateMoneyOnHUD ();
    }

    ///// Buy //////
    static public bool Buy(int amount) {
        return Singleton._Buy(amount);
    }

    bool _Buy(int amount) {
        if (_money >= amount) {
            _money -= amount;
			MoneyController.Instance.UpdateMoneyOnHUD ();
            return true;
        } else
            return false;
    }

    ///// TestBuy //////
    static public bool TestBuy(int amount) {
        return Singleton._TestBuy(amount);
    }

    bool _TestBuy(int amount) {
        if (_money >= amount) {
            return true;
        } else
            return false;
    }

    ///// Reset /////

    static public void reset() {
        Singleton._reset();
		MoneyController.Instance.UpdateMoneyOnHUD ();
    }

    void _reset() {
        _money = moneyStart;
        _priceCamera = priceCamera;
        _priceGuard = priceGuard;
        _priceLaser = priceLaser;
		MoneyController.Instance.UpdateMoneyOnHUD ();
    }

    ///// Camera /////
    static public int cameraPrice { get { return Singleton._priceCamera; } }
    static public bool cameraTestBuy() { return Singleton._cameraTestBuy(); }
    bool _cameraTestBuy() { return _TestBuy(_priceCamera); }
    static public bool cameraBuy() { return Singleton._cameraBuy(); }
    bool _cameraBuy() { return _Buy(_priceCamera); }

    ///// Guard /////
    static public int guardPrice { get { return Singleton._priceGuard; } }
    static public bool guardTestBuy() { return Singleton._guardTestBuy(); }
    bool _guardTestBuy() { return _TestBuy(_priceGuard); }
    static public bool guardBuy() { return Singleton._guardBuy(); }
    bool _guardBuy() { return _Buy(_priceGuard); }

    ///// Laser /////
    static public int laserPrice { get { return Singleton._priceLaser; } }
    static public bool laserTestBuy() { return Singleton._laserTestBuy(); }
    bool _laserTestBuy() { return _TestBuy(_priceLaser); }
    static public bool laserBuy() { return Singleton._laserBuy(); }
    bool _laserBuy() { return _Buy(_priceLaser); }

}
