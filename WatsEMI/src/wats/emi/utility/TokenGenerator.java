package wats.emi.utility;

import java.util.Random;

public final class TokenGenerator {
    private TokenGenerator() {
    }

    private static final byte[] TOKEN = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
            'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
            's', 't', 'u', 'v', 'w', 'x', 'y', 'z',           //26
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I',
            'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
            'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',           //26
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' //10
            //, '-', '.', '!', '%', '*', '_', '+', '`', '\'', '~'
    }; //12

    private static final int TOKEN_LENGTH = TOKEN.length;

    /**
     *
     * Returns a String objects representing a random token with the specific length.
     *
     * the Token string representation is as described by this BNF
     *
     *      Token = aLength(ALPHANUM)
     *      ALPHANUM  =  ALPHA / DIGIT
     *      DIGIT =  "0" / "1" / "2" / "3" / "4" / "5" / "6" / "7" / "8" / "9"
     *      ALPHA =  %x41-5A / %x61-7A   ; A-Z / a-z
     *
     * @param aLength the length of Token String
     * @return a String with token
     */
    public static String generateToken(int aLength) {
        byte[] myRandomBytes = new byte[aLength];

        Random myRandom = new Random();
        for (int i = 0; i < aLength; i++) {
            int myRandomInt = myRandom.nextInt();
            int myRandomIndex = (myRandomInt % TOKEN_LENGTH + TOKEN_LENGTH) % TOKEN_LENGTH;
            myRandomBytes[i] = TOKEN[myRandomIndex];
        }

        return new String(myRandomBytes);
    }
}