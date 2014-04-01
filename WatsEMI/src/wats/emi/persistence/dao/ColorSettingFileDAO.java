package wats.emi.persistence.dao;

import org.hibernate.SessionFactory;
import wats.emi.persistence.SessionFactoryGenerator;
import wats.emi.persistence._ColorSettingFile;

/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-3-30
 * Time: 上午4:46
 * To change this template use File | Settings | File Templates.
 */
public class ColorSettingFileDAO extends GenericDAO<_ColorSettingFile> {
    public ColorSettingFileDAO() {
        super(SessionFactoryGenerator.getSessionFactory());
    }

    public ColorSettingFileDAO(GenericDAO dao) {
        super(dao);
    }
}
