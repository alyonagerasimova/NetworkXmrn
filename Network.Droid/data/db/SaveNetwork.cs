using Android.Content;
using Android.Database.Sqlite;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace NueralNetrwork.Network.db
{
    class SaveNetwork
    {
    private static String DATABASE_NAME = "network.db"; // название бд
    private static int VERSION = 1; // версия базы данных
    public static String TABLE = "network"; // название таблицы в бд
    public static String COLUMN_ID = "_id";
    public static String COLUMN_NAME = "name";
    public static String COLUMN_LINE_BIAS = "lineBias";
    public static String COLUMN_STOLB_BIAS = "stolbBias";
    public static String COLUMN_LINE_WEIGHTS0 = "lineWeights0";
    public static String COLUMN_LINE_WEIGHTS1 = "lineWeights1";
    public static String COLUMN_STOLB_WEIGHTS0 = "stolbWeights0";
    public static String COLUMN_STOLB_WEIGHTS1 = "stolbWeights1";
    public static String COLUMN_OUTPUT_VALUES = "outputValues";
    public static String COLUMN_HIDDEN_VALUES = "hiddenValues";
    public static String COLUMN_NUMBER_HIDDEN = "numberHidden";
    public static String COLUMN_NUMBER_CYCLE = "numberCycle";
    public static String COLUMN_NUMBER_LEARNING = "numberLearning";
    public static String COLUMN_NUMBER_ERROR = "numberError";
    private static List<Network> networks = new List<Network>();

        public static void add(Network network)
        {
            networks.Add(network);
        }
        public static List<Network> getListData()
        {
            return networks;
        }

        public SaveNetwork(Context context)
        {    
        }

        public void saveNewNetwork(Network network)
        {
            network.setFlagName("network");
            network.setNameNetwork("network");
            add(network);
            SQLiteDatabase db;
            string cs = @"URI=file:C:\Users\Jano\Documents\test.db";

            using var con = new SqliteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);
            ContentValues values = new ContentValues();
            values.put(COLUMN_NAME, network.getFlagName());
            values.put(COLUMN_NUMBER_HIDDEN, network.getNumberHiddenNeurons());
            values.put(COLUMN_NUMBER_LEARNING, network.getLearningRateFactor());
            values.put(COLUMN_NUMBER_CYCLE, network.getNumberCycles());
            values.put(COLUMN_NUMBER_ERROR, network.getError());
            double[] doublesHidden = network.getHiddenValues();
            double[] doubleOutput = network.getOutputValues();
            double[][] bias = network.getBias();
            double[][][] weights = network.getWeights();
            ByteBuffer bb = ByteBuffer.allocate(doublesHidden.length * 8);
            for (double d : doublesHidden)
            {
                bb.putDouble(d);
            }
            byte[] bytes = bb.array();
            values.put(COLUMN_HIDDEN_VALUES, bytes);
            ByteBuffer bbb = ByteBuffer.allocate(doubleOutput.length * 8);
            for (double d : doubleOutput)
            {
                bbb.putDouble(d);
            }
            byte[] bytess = bb.array();
            values.put(COLUMN_OUTPUT_VALUES, bytess);

            double[] lineBias = bias[0];
            ByteBuffer bbbb = ByteBuffer.allocate(lineBias.length * 8);
            for (double d : lineBias)
            {
                bbbb.putDouble(d);
            }
            byte[] lineBiasByte = bbbb.array();
            values.put(COLUMN_LINE_BIAS, lineBiasByte);

            double[] stolbBias = bias[1];
            ByteBuffer bbbbb = ByteBuffer.allocate(stolbBias.length * 8);
            for (double d : stolbBias)
            {
                bbbbb.putDouble(d);
            }
            byte[] stolbBiasByte = bbbbb.array();
            values.put(COLUMN_STOLB_BIAS, stolbBiasByte);

            weights[0] = new double[400][doublesHidden.length];
            weights[1] = new double[doublesHidden.length][doubleOutput.length];

            double[] weightsLine0 = weights[0][0];
            ByteBuffer bbbbbb = ByteBuffer.allocate(weightsLine0.length * 8);
            for (double d : weightsLine0)
            {
                bbbbbb.putDouble(d);
            }
            byte[] lineWeight0Byte = bbbbbb.array();
            values.put(COLUMN_LINE_WEIGHTS0, lineWeight0Byte);

            double[] weightsStolb0 = weights[0][1];
            ByteBuffer bbbbbbb = ByteBuffer.allocate(weightsStolb0.length * 8);
            for (double d : weightsStolb0)
            {
                bbbbbbb.putDouble(d);
            }
            byte[] stolbWeight0Byte = bbbbbbb.array();
            values.put(COLUMN_STOLB_WEIGHTS0, stolbWeight0Byte);


            double[] weightsLine1 = weights[1][0];
            ByteBuffer bbbbbbbb = ByteBuffer.allocate(weightsLine1.length * 8);
            for (double d : weightsLine1)
            {
                bbbbbbbb.putDouble(d);
            }
            byte[] lineWeight1Byte = bbbbbbbb.array();
            values.put(COLUMN_LINE_WEIGHTS1, lineWeight1Byte);


            double[] weightsStolb1 = weights[1][1];
            ByteBuffer bbbbbbbbb = ByteBuffer.allocate(weightsStolb1.length * 8);
            for (double d : weightsStolb1)
            {
                bbbbbbbbb.putDouble(d);
            }
            byte[] stolbWeight1Byte = bbbbbbbbb.array();
            values.put(COLUMN_STOLB_WEIGHTS1, stolbWeight1Byte);


            db.insert(TABLE, null, values);
            db.close();


        }

        @Override
    public void onCreate(SQLiteDatabase db)
        {
            cmd.CommandText("CREATE TABLE network (" + COLUMN_ID
                    + " INTEGER PRIMARY KEY AUTOINCREMENT," + COLUMN_NAME
                    + " TEXT, " + COLUMN_NUMBER_HIDDEN + " INTEGER," + COLUMN_NUMBER_LEARNING
                    + " REAL," + COLUMN_NUMBER_CYCLE + " INTEGER,"
                    + COLUMN_NUMBER_ERROR + " REAL," + COLUMN_HIDDEN_VALUES + " BLOB," + COLUMN_OUTPUT_VALUES + " BLOB," +
                    COLUMN_LINE_BIAS + " BLOB," + COLUMN_STOLB_BIAS + " BLOB," + COLUMN_LINE_WEIGHTS0 + " BLOB," + COLUMN_STOLB_WEIGHTS0 + " BLOB," +
                    COLUMN_LINE_WEIGHTS1 + " BLOB," + COLUMN_STOLB_WEIGHTS1 + " BLOB);");
            //        db.execSQL("INSERT INTO " + TABLE + " (" + COLUMN_NAME
            //                + ", " + COLUMN_NUMBER_HIDDEN + "," + COLUMN_NUMBER_LEARNING + ","
            //                + COLUMN_NUMBER_CYCLE + "," + COLUMN_NUMBER_ERROR + "," + COLUMN_NETWORK + ")  VALUES ('Нейронная сеть 1', 100, 0.1, 5, null, null);");
        }

        @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.execSQL("DROP TABLE IF EXISTS " + TABLE);
            onCreate(db);
        }
    }
}