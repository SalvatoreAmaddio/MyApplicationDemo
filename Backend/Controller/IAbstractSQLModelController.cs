﻿using Backend.Database;
using Backend.Model;
using Backend.Recordsource;

namespace Backend.Controller
{
    public interface IAbstractSQLModelController
    {
        /// <summary>
        /// An instance of a <see cref="IAbstractDatabase"/>.
        /// </summary>
        /// <value>An object representing a Database that extends <see cref="AbstractDatabase"/></value>
        public IAbstractDatabase Db { get; }

        /// <summary>
        /// The current selected record.
        /// </summary>
        /// <value>An object that implements <see cref="ISQLModel"/> or extends <see cref="AbstractSQLModel"/>, which represents the current selected record</value>
        public ISQLModel? CurrentModel { get; set; }

        /// <summary>
        /// A recordsource object that old the collection of records.
        /// </summary>
        /// <value>A RecordSource</value>
        public RecordSource Source { get; }

        /// <summary>
        /// Gets and Sets whether or no a new Record can be added. Default value is True.
        /// </summary>
        /// <value>True if you can add new records.</value>
        public bool AllowNewRecord { get; set; }

        /// <summary>
        /// It tells the RecordSource's Enumerator to go to the next available record.
        /// see <see cref="RecordSource"/>
        /// </summary>
        public void GoNext();

        /// <summary>
        /// It tells the RecordSource's Enumerator to go to the previous available record.
        /// see <see cref="RecordSource"/>
        /// </summary>
        public void GoPrevious();

        /// <summary>
        /// It tells the RecordSource's Enumerator to go to the last record.
        /// see <see cref="RecordSource"/>
        /// </summary>
        public void GoLast();

        /// <summary>
        /// It tells the RecordSource's Enumerator to go to the first record.
        /// see <see cref="RecordSource"/>
        /// </summary>
        public void GoFirst();

        /// <summary>
        /// It tells the RecordSource's Enumerator to go to a given record based on its zero-based position.
        /// see <see cref="RecordSource"/>
        /// </summary>
        /// <param name="index">the zero-based index of the Record to go to.</param>
        public void GoAt(int index);

        /// <summary>
        /// It finds the given record and tells the RecordSource's Enumerator to go to a its zero-based position.
        /// see <see cref="RecordSource"/>
        /// </summary>
        /// <param name="record">the record to move to.</param>
        public void GoAt(ISQLModel? record);

        /// <summary>
        /// It tells the RecordSource's Enumerator that a new record will be added.
        /// see <see cref="RecordSource"/>
        /// </summary>
        public void GoNew();

        /// <summary>
        /// It performs a Insert or Update Statement based on the <see cref="CurrentModel"/>'s <see cref="INotifier.IsDirty"/> property and the <see cref="ISQLModel.IsNewRecord"/> method.
        /// <list type="bullet">
        /// <item>
        /// <term>update</term>
        /// <description> (IsDirty = True) AND (IsNewRecord() = False)</description>
        /// </item>
        /// <item>
        /// <term>insert</term>
        /// <description> (IsDirty = True) AND (IsNewRecord() = True)</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <returns>true if the record was successfully altered.</returns>
        /// <exception cref="NoModelException">Thrown if the <see cref="Model"/> is null.</exception>
        public bool AlterRecord(string? sql = null, List<QueryParameter>? parameters = null);

        /// <summary>
        /// It deletes the <see cref="CurrentModel"/> from the database.
        /// </summary>
        /// <exception cref="NoModelException">Thrown if the <see cref="Model"/> is null.</exception>
        public void DeleteRecord(string? sql = null, List<QueryParameter>? parameters = null);

        /// <summary>
        /// Override this method to return a zero-based index. This index fetches an 
        /// IAbstractDatabase from the <see cref="ControllerManager"/> and allow the Constructor to initialize the <see cref="Db"/> and <see cref="Source"/> properties.
        /// <para/>
        /// For Example:
        /// <code>
        /// public override int DatabaseIndex => 0; //fetch the first IAbstractDatabase
        /// </code>
        /// </summary>
        /// <returns>A zero-based integer that identifies the IAbstractDatabase in the <see cref="ControllerManager"/></returns>
        public int DatabaseIndex { get; }

        /// <summary>
        /// Gets a string representing the Record position to be displayed
        /// </summary>
        public string Records { get; }
    }
}