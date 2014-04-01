package wats.emi.persistence;

import org.hibernate.SessionFactory;
import org.hibernate.boot.registry.StandardServiceRegistryBuilder;
import org.hibernate.cfg.Configuration;
import org.hibernate.service.ServiceRegistry;

public class SessionFactoryGenerator {
    private static SessionFactory sessionFactory;
    private static String schema = "EMI";

    //Used for test database
    public static void setSchema(String schema) {
        SessionFactoryGenerator.schema = schema;
    }

    public static SessionFactory getSessionFactory() {
        if (sessionFactory == null) {
            Configuration configuration = new Configuration();
            configuration.addPackage(SessionFactoryGenerator.class.getPackage().getName());

            configuration.addAnnotatedClass(_Analysis.class);
            configuration.addAnnotatedClass(_AnalysisSetting.class);
            configuration.addAnnotatedClass(_AnalysisType.class);
            configuration.addAnnotatedClass(_ChannelSettingFile.class);
            configuration.addAnnotatedClass(_ColorSettingFile.class);
            configuration.addAnnotatedClass(_EMIFile.class);
            configuration.addAnnotatedClass(_EquipmentParameterFile.class);
            configuration.addAnnotatedClass(_LinkConfigurationFile.class);
            configuration.addAnnotatedClass(_Region.class);
            configuration.addAnnotatedClass(_Site.class);
            configuration.addAnnotatedClass(_Task.class);
            configuration.addAnnotatedClass(_User.class);

            configuration.setProperty("hibernate.default_schema", schema);
            configuration.configure();
            ServiceRegistry serviceRegistry = new StandardServiceRegistryBuilder().applySettings(
                    configuration.getProperties()).build();
            sessionFactory = configuration.buildSessionFactory(serviceRegistry);
        }

        return sessionFactory;
    }
}
