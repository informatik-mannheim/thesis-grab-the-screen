package com.example.valentinaburjan.grabthescreen_androidapp;

import android.app.Activity;
import android.os.Bundle;
import android.os.RemoteException;
import android.view.Menu;
import android.view.MenuItem;

import com.estimote.sdk.Beacon;
import com.estimote.sdk.BeaconManager;
import com.estimote.sdk.Region;
import com.estimote.sdk.Utils;
import com.estimote.sdk.Utils.Proximity;
import com.estimote.sdk.utils.EstimoteBeacons;

import java.util.List;


public class MainActivity extends Activity implements BeaconManager.RangingListener, BeaconManager.ServiceReadyCallback {

    private BeaconManager _manager;
    private Region _allEstimoteBeacons;


    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        _allEstimoteBeacons = new Region("regionId", EstimoteBeacons.ESTIMOTE_PROXIMITY_UUID, null, null);

        _manager = new BeaconManager(this);
        _manager.setRangingListener(this);

    }

    @Override
    protected void onStart() {
        super.onStart();
        _manager.connect(this);
    }

    @Override
    protected void onStop() {
        try {
            super.onStop();
            _manager.stopRanging(_allEstimoteBeacons);
            _manager.disconnect();
        } catch (RemoteException e) {
            e.printStackTrace();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }


    /**
     * This is where the magic happens.
     * region - Region that was used for ranging.
     * beacons - List of beacons sorted by accuracy.
     */
    @Override
    public void onBeaconsDiscovered(Region region, List<Beacon> beacons) {
        //Log.d("TEST", beacons.toString());

        for (Beacon beacon : beacons) {
            if (Utils.computeProximity(beacon).equals(Proximity.IMMEDIATE)) {
                System.out.println("All beacons are in immediate proximity. Distance: " + Utils.computeProximity(beacon));
            } else {
                System.out.println("Minimum one beacon is NOT in immediate proximity.Distance: " + getDistance(beacon.getRssi(), beacon.getMeasuredPower()));
            }

        }

        //TODO: recognize the one beacon that is the table
        //TODO: find out the actual distance to that beacon (Hint: Utilities)
        //TODO: react if in proximity "IMMEDIATE"
    }

    double getDistance(int rssi, int txPower) {
    /*
     * RSSI = TxPower - 10 * n * lg(d)
     * n = 2 (in free space)
     *
     * d = 10 ^ ((TxPower - RSSI) / (10 * n))
     */

        return Math.pow(10d, ((double) txPower - rssi) / (10 * 2));
    }

    /**
     * Don't touch
     */
    @Override
    public void onServiceReady() {
        try {
            _manager.startRanging(_allEstimoteBeacons);
        } catch (RemoteException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        // When no longer needed.
        _manager.disconnect();
    }

}

