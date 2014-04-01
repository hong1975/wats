package wats.emi.utility;

import java.util.Date;
import java.text.SimpleDateFormat;

public class IDGenerator {
	private static long lastID;
	private static final SimpleDateFormat sdf;
	
	static {
		sdf = new SimpleDateFormat("yyyyMMddHHmmssSSS");
	}
	
	public synchronized static long getID() {
		long newID = Long.parseLong(sdf.format(new Date()), 10) * 100;
		while (newID <= lastID)
			newID++;
		lastID = newID;
		
		return newID;
	}
	
	public static void main(String[] args) {
		for (int i = 0; i < 1000; i++)
			System.out.println(getID());
	}

}
