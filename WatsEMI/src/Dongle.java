/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-1-5
 * Time: 下午11:36
 * To change this template use File | Settings | File Templates.
 */
public class Dongle {
    static {
        System.loadLibrary("DongleImpl");
    }

    /**
     * Get available user count from dongle
      * @return
     *  -1: initialize failure
     *  -2: software expired
     *  -3: get user count failure
     *  >= 0: available user count
     */
    public native int getAvailableUserCount();
}
