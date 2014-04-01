package wats.emi.persistence.dao;

import org.hibernate.SessionFactory;
import wats.emi.persistence.SessionFactoryGenerator;
import wats.emi.persistence._LinkConfigurationFile;

/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-3-30
 * Time: 上午4:47
 * To change this template use File | Settings | File Templates.
 */
public class LinkConfigurationFileDAO extends GenericDAO<_LinkConfigurationFile> {
    public LinkConfigurationFileDAO() {
        super(SessionFactoryGenerator.getSessionFactory());
    }

    public LinkConfigurationFileDAO(GenericDAO dao) {
        super(dao);
    }
}
